using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/schoolclasses")]
    public class SchoolClassController : ControllerBase
    {
        private IConfiguration Config { get; }
        private ApplicationDbContext DbContext { get; }
        //private readonly ILogger<StudentsController> _logger;

        public SchoolClassController(IConfiguration config, ApplicationDbContext context) : base()//ILogger<StudentsController> logger)
        {
            //_logger = logger;
            DbContext = context;
            Config = config;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SchoolClass>>> GetAll()
        {
            var result = await DbContext.SchoolClasses.ToListAsync();

            return Ok(result);
        }
    }
}

