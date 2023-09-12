using System.Linq;
using AutoMapper;
using Core.ExtensionMethods;
using Core.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Scripting.Utils;
using Persistence;
using Shared.Dtos;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("/grades")]
    public class GradeController : ControllerBase
    {
        private readonly GradeCalculator _gradeCalculator;
        private readonly IMapper _mapper;
        private IConfiguration Config { get; }
        private ApplicationDbContext DbContext { get; }
        //private readonly ILogger<StudentsController> _logger;
        public GradeController(IConfiguration config, ApplicationDbContext dbContext, IMapper mapper, GradeCalculator gradeCalculator) 
        {
            this.Config = config;
            this.DbContext = dbContext;
            _mapper = mapper;
            _gradeCalculator = gradeCalculator;       
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Get all grades from backend
        /// </summary>
        /// <returns>IEnumberable<GradeGetDto> result </returns>
        public async Task<ActionResult<IEnumerable<GradeGetDto>>> GetAllGradesAsync()
        {
            var grades = await DbContext.Grades
            .Include(g => g.Student)
            .Include(g => g.GradeKind)
            .ToListAsync();
            
            var result = grades.Select(grade => _mapper.Map<GradeGetDto>(grade)).ToList();

            return Ok(result);
        }

        [HttpGet("/gradesForClass/{schoolClassId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Get all grades from backend
        /// </summary>
        /// <returns>IEnumberable<GradeGetDto> result </returns>
        public async Task<ActionResult<IEnumerable<GradeGetDto>>> GetGradesForSchoolClassAsync(int schoolClassId)
        {
            var grades = await DbContext.Grades
            .Include(g => g.Student)
            .Include(g => g.GradeKind)
            .Where(g => g.Student!.SchoolClassId == schoolClassId)
            .ToListAsync();
            
            var result = grades.Select(grade => _mapper.Map<GradeGetDto>(grade)).ToList();

            return Ok(result);
        }
        
        private bool IsStudentInSchoolClass(SchoolClass sc, int studentId)
        {
            return sc.Students.Any(s => s.Id == studentId);
        }
        
        [HttpGet("/calcForClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Calculates the grades for one year as with script.
        /// </summary>
        /// <param name="schoolClassId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<GradeGetDto>>> CalculateGradesForClassAsync(int schoolClassId, int subjectId)
        {
            var dbSchoolClass = await DbContext.SchoolClasses.SingleOrDefaultAsync(sc => sc.Id == schoolClassId);

            if(dbSchoolClass == null)
            {
                return BadRequest("SchoolClass not found!");
            }

            var dbGrades = await DbContext.Grades
                .Where(g => g.SubjectId == subjectId)
                .ToListAsync();

            if (dbGrades.IsNullOrEmpty())
            {
                return BadRequest("No Grades found!");
            }

            var keys = await DbContext.GradeKeys.Include(gk => gk.SchoolClasses).Select(gk => gk).ToListAsync();
                
                
            var keyForClassExists = keys
            .Any(g => g.SubjectId == subjectId && g.SchoolClasses.Contains(dbSchoolClass));

            if (!keyForClassExists)
            {
                return BadRequest($"No Key for this class found!");
            }

            var result =  (await _gradeCalculator.CalculateKeysForClassAndSubject(schoolClassId, subjectId)).ToList();

            if (result == null || result.Count() == 0)
            {
                return BadRequest("Error in Calculation");
            }

            try
            {
                await DbContext.Grades.AddRangeAsync(result!);
                await DbContext.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                 return BadRequest(ex);
            }

            var ret = result.Select(g => _mapper.Map<GradeGetDto>(g)).OrderBy(g => g.Id).ToList();
            return Ok(ret);
        }

        [HttpGet("/keys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Get all GradeKeys from backend
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<GradeKeyGetDto>>> GetAllGradeKeyAsync()
        {
            var gradeKeys = await DbContext.GradeKeys.Include(k => k.UsedKinds).ToListAsync();
            
            var result = gradeKeys.Select(key => _mapper.Map<GradeKeyGetDto>(key)).ToList();
            return Ok(result);
        }


        [HttpGet("/kinds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Get all GradeKinds from backend
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<GradeKindGetDto>>> GetAllKindsAsync()
        {
            var gradeKinds = await DbContext.GradeKinds.ToListAsync();
            var result = gradeKinds.Select(kind => _mapper.Map<GradeKindGetDto>(kind));
            return Ok(result);
        }


        [HttpPut("/keys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GradeKey>> UpdateKeyAsync(GradeKey gradeKey)
        {
            if (gradeKey == null)
            {
                return BadRequest("Grade Key Required");
            }

            var gradeKeyDb = await DbContext.GradeKeys.GetByIdAsync(gradeKey.Id);

            if (gradeKeyDb == null)
            {
                return BadRequest("GradeKey not found!");
            }

            gradeKeyDb.SubjectId = gradeKey.SubjectId;

            gradeKeyDb.Calculation = gradeKey.Calculation;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok(gradeKeyDb);
        }
        
        [HttpPost("/keys")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GradeKeyGetDto>> AddKeyAsync(GradeKeyPostDto gradeKeyPostDto)
        {
            if (gradeKeyPostDto == null)
            {
                return BadRequest("PostDto required!");
            }
            var gradeKeyToAddDb = _mapper.Map<GradeKey>(gradeKeyPostDto); 

            var subject = await DbContext.Subjects.SingleOrDefaultAsync(s => s.Id == gradeKeyPostDto.SubjectId);

            if (subject == default(Subject))
            {
                return BadRequest("Subject does not exist!");
            }

            gradeKeyToAddDb.Subject = subject;
            var kinds = gradeKeyToAddDb.UsedKinds.ToList();        
            var dbKinds = await DbContext.GradeKinds.ToListAsync();
            //var kindsExistsName = dbKinds.Select(k => k.Name).Intersect(gradeKeyPostDto.UsedKinds).ToList();
            //var kindsToAdd = kinds.Where(k => !kindsExistsName.Any(e => e == k.Name)).ToList();

            gradeKeyToAddDb.UsedKinds = kinds;

            var schoolClassesDb = DbContext.SchoolClasses.ToList();
            var schoolClasses = gradeKeyToAddDb.SchoolClasses!.Select(s => s.Name).ToList();



            if (schoolClasses.Except(schoolClassesDb.Select(s => s.Name)).Count() > 0)
            {
                return BadRequest("Some Schoolclasses may not exists");
            }

            gradeKeyToAddDb.SchoolClasses = new List<SchoolClass>();
            foreach (var item in schoolClasses)
            {
                gradeKeyToAddDb.SchoolClasses.Add(schoolClassesDb.Single(s => s.Name == item));
            }

            gradeKeyToAddDb.UsedKinds = new List<GradeKind>();

            foreach (var item in kinds)
            {
                gradeKeyToAddDb.UsedKinds.Add(dbKinds.Single(k => k.Name == item.Name));
            }

            try
            {
                await DbContext.GradeKeys.AddAsync(gradeKeyToAddDb);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return NoContent();
        }
    }
}
