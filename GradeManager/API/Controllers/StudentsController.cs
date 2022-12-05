using Core.Contracts;
using Core.Contracts.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/schoolclasses")]
    public class StudentsController : ControllerBase
    {
        private IConfiguration Config { get; }
        private IUnitOfWork UnitOfWork { get; }
        //private readonly ILogger<StudentsController> _logger;

        public StudentsController(IConfiguration config, IUnitOfWork unitOfWork) : base()//ILogger<StudentsController> logger)
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

