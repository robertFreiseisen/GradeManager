using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Dtos;
using Shared.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("/schoolclasses")]
    public class SchoolClassController : ControllerBase
    {
        private IConfiguration Config { get; }
        private ApplicationDbContext DbContext { get; }
        //private readonly ILogger<StudentsController> _logger;
        private readonly IMapper _mapper;
        public SchoolClassController(IConfiguration config, ApplicationDbContext context, IMapper mapper) : base()
        {
            //_logger = logger;
            DbContext = context;
            _mapper = mapper;
            Config = config;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SchoolClassGetDto>>> GetAll()
        {
            var schoolClasses = await DbContext.SchoolClasses.ToListAsync();

            var result = schoolClasses.Select(sc => _mapper.Map<SchoolClassGetDto>(sc)).ToList();

            return Ok(result);
        }
    }
}

