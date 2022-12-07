using Core.Contracts;
using Core.Contracts.Entities;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GradeGetDto>>> GetAllGradesAsync()
        {
            var grades = await UnitOfWork.GradeRepository.GetAllAsync();

            var dtos = grades
                .Select(g => new GradeGetDto(g.Id, g.Graduate, g.SubjectId, g.TeacherId, g.StudentId, g.Note));
            
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<GradeKeyGetDto>> AddKeyAsync()
        {

        }
    }

    public record GradeGetDto(int Id, int Graduate, int SubjectId, int TeacherId, int StudentId, string Note);
    public record GradeKeyGetDto(int Id, int ScriptType, string Script, int SubjectId);
    public record GradeKeyPostDto(int ScriptType, string Script);
}
