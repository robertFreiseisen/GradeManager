using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/grades")]
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
            var grades = await UnitOfWork.GradeRepository.GetAllAsync();

            return Ok(grades);
        }

        [HttpGet("/keys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GradeKey>>> GetAllGradeKeyAsync()
        {
            var grades = await UnitOfWork.GradeKeyRepository.GetAllAsync();

            return Ok(grades);
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
