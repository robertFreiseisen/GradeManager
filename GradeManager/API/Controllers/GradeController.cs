using Core.Contracts;
using Core.Logic;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("/grades")]
    public class GradeController : ControllerBase
    {
        private IConfiguration Config { get; }
        private IUnitOfWork UnitOfWork { get; }
        //private readonly ILogger<StudentsController> _logger;

        public GradeController(IConfiguration config, IUnitOfWork unitOfWork) : base()//ILogger<StudentsController> logger)
        {
            //_logger = logger;
            UnitOfWork = unitOfWork;
            Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Grade>>> GetAllGradesAsync()
        {
            var grades = await UnitOfWork.GradeRepository.GetAllWithStudentsAsync();
            var gradesReturn = grades.Distinct();

            return Ok(gradesReturn);
        }

        [HttpGet("/calcForClass/{schoolClassId}/{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Grade>>> CalculateGradesForClassAsync(int schoolClassId, int subjectId)
        {
            var calc = new GradeCalculator();

            var result = await calc.CalculateKeysForClassAndSubject(schoolClassId, subjectId, UnitOfWork);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("/keys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GradeKey>>> GetAllGradeKeyAsync()
        {
            var grades = await UnitOfWork.GradeKeyRepository.GetAllAsync();

            return Ok(grades);
        }


        [HttpGet("/schoolclasses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SchoolClass>>> GetAllSchoolclassesAsync()
        {
            var students = await UnitOfWork.SchoolClassRepository.GetAllAsync();

            return Ok(students);
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

            var gradeKeyDb = await UnitOfWork.GradeKeyRepository.GetByIdAsync(gradeKey.Id);

            if (gradeKeyDb == null)
            {
                return BadRequest("GradeKey not found!");
            }

            gradeKeyDb.SubjectId = gradeKey.SubjectId;

            gradeKeyDb.Calculation = gradeKey.Calculation;

            try
            {
                await UnitOfWork.SaveChangesAsync();
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
        public async Task<ActionResult<GradeKey>> AddKeyAsync(GradeKey gradeKeyPostDto)
        {
            if (gradeKeyPostDto == null)
            {
                return BadRequest("PostDto required!");
            }

            var keyToAdd =
                new GradeKey
                {
                    TeacherId = gradeKeyPostDto.TeacherId,
                    Name = gradeKeyPostDto.Name,
                    ScriptType = gradeKeyPostDto.ScriptType,
                    Calculation = gradeKeyPostDto.Calculation
                };

            try
            {
                await UnitOfWork.GradeRepository.AddGradeKeyAsync(keyToAdd);
                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
