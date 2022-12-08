using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/schoolclasses")]
    public class SchoolClassController : ControllerBase
    {
        private IConfiguration Config { get; }
        private IUnitOfWork UnitOfWork { get; }
        //private readonly ILogger<StudentsController> _logger;

        public SchoolClassController(IConfiguration config, IUnitOfWork unitOfWork) : base()//ILogger<StudentsController> logger)
        {
            //_logger = logger;
            UnitOfWork = unitOfWork;
            Config = config;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SchoolClass>>> GetAll()
        {
            var result = await UnitOfWork.SchoolClassRepository.GetAllAsync();

            return Ok(result);
        }
    }
}

