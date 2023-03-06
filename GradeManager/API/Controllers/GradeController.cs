using System.Linq;
using AutoMapper;
using Core.ExtensionMethods;
using Core.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var grades = await DbContext.Grades.Include(g => g.GradeKind).ToListAsync();

            var result = grades.Select(grade => _mapper.Map<GradeGetDto>(grade)).ToList();

            return Ok(result);
        }
        
        [HttpPost("/addKey")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /// <summary>
        /// Adds GradeKey with GradeKeyPostDto
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddGradeKeyAsync(GradeKeyPostDto key)
        {
            var dbKey = await DbContext.GradeKeys
            .SingleOrDefaultAsync(k => k.Name == key.Name && k.TeacherId == key.TeacherId);
            
            if (dbKey != null)
            {
                return BadRequest($"GradeKey {key.Name} already exists!");    
            }

            try
            {
                var keyToAdd = _mapper.Map<GradeKey>(key);

                await DbContext.GradeKeys.AddAsync(keyToAdd);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {                
                return BadRequest(e.Message);
            }
            return Ok();
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
            var grades = await _gradeCalculator.CalculateKeysForClassAndSubject(schoolClassId, subjectId);
            if (grades == null)
            {
                return BadRequest();
            }

            var result = grades.Select(grade => _mapper.Map<GradeGetDto>(grade)).ToList();

            return Ok(result);
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

            var kinds = gradeKeyPostDto.UsedKinds.Select(kind => _mapper.Map<GradeKind>(kind));

            var kindsExists = await DbContext.GradeKinds.IntersectBy(kinds, k => k).ToListAsync();           

            var kindsToAdd = kinds.Except(kindsExists).ToList();

            if (kindsToAdd.Count() != 0)
            {
                try
                {
                    await DbContext.GradeKinds.AddRangeAsync(kindsToAdd);   
                    await DbContext.SaveChangesAsync();   
                }
                catch (System.Exception e)
                {
                    return BadRequest(e);
                    throw;
                }
            }

            var keyToAdd = _mapper.Map<GradeKey>(gradeKeyPostDto);
            try
            {
                await DbContext.GradeKeys.AddAsync(keyToAdd);
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
