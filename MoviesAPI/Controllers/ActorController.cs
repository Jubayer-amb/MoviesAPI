using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DB;
using MoviesAPI.DTOs;

namespace MoviesAPI.Controllers
{
    [Route("api/actors")]
    [ApiController]

    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ActorController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var actors = await _dbContext.Actors.ToListAsync();
            return _mapper.Map<List<ActorDTO>>(actors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put([FromForm] ActorDTO actorDTO, int Id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
