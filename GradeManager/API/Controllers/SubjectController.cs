using Core.ExtensionMethods;
using Core.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("/subjects")]
    public class SubjectController : ControllerBase
    {
        private IConfiguration Config { get; }
        private ApplicationDbContext DbContext { get; }
        //private readonly ILogger<StudentsController> _logger;
        public SubjectController(IConfiguration config, ApplicationDbContext dbContext,  GradeCalculator gradeCalculator) : base()//ILogger<StudentsController> logger)
        {
            //_logger = logger;
            DbContext = dbContext;
            Config = config;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetAllGradesAsync()
        {
            var subjects = await DbContext.Subjects.ToListAsync();

            return Ok(subjects);
        }
    

        [HttpGet("/teacher/{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetByTeacher(int teacherId)
        {
            var teacher = await DbContext.Teachers
            .Include(t => t.Subjects)
            .SingleOrDefaultAsync(t => t.Id == teacherId);

            if (teacher == null)
            {
                return BadRequest("Teacher doesn't exist!");
            }

            var subjects = teacher!.Subjects;

            return Ok(subjects);
        }
    }
}
