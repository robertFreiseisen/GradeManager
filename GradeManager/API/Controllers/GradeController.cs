using Core.ExtensionMethods;
using Core.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/grades")]
    public class GradeController : ControllerBase
    {
        private readonly GradeCalculator gradeCalculator;

        private IConfiguration Config { get; }
        private ApplicationDbContext DbContext { get; }
        //private readonly ILogger<StudentsController> _logger;

        public GradeController(IConfiguration config, ApplicationDbContext dbContext,  GradeCalculator gradeCalculator) : base()//ILogger<StudentsController> logger)
        {
            //_logger = logger;
            DbContext = dbContext;
            this.gradeCalculator = gradeCalculator;
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
            var grades = await DbContext.Grades.ToListAsync();

            return Ok(grades);
        }

        [HttpGet("/calcForClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Grade>>> CalculateGradesForClassAsync(int schoolClassId, int subjectId)
        {
            var result = await gradeCalculator.CalculateKeysForClassAndSubject(schoolClassId, subjectId);
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
            var grades = await DbContext.GradeKeys!.ToListAsync();

            return Ok(grades);
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
                await DbContext.GradeKeys.AddAsync(keyToAdd);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
