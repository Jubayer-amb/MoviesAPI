using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DB;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]


    public class GenresController:ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenresController(ILogger<GenresController> logger, ApplicationDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PagedResultDTO>> Get([FromQuery] PaginationDTO paginationDTO)
        {
           PagedResultDTO genreDTO = new();
            IQueryable<Genre> queryable = _dbContext.Genres.AsQueryable();
            var genres = await queryable.OrderBy(x => x.Name)
                .Skip((paginationDTO.page -1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage)
                .ToListAsync();
            genreDTO.data = _mapper.Map<List<GenreDTO>>(genres);
            genreDTO.totalData = await queryable.CountAsync();


            return Ok(genreDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = _mapper.Map<Genre>(genreCreationDTO);
            _dbContext.Add(genre);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult> Get(int Id)
        {
            //var genre = await _dbContext.Genres.FindAsync(Id);
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == Id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, GenreCreationDTO genreCreationDTO)
        {
            var genre = await _dbContext.Genres.FindAsync(Id);
            if(genre == null) return NotFound();
            genre = _mapper.Map(genreCreationDTO, genre);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var genre = await _dbContext.Genres.AnyAsync(x => x.Id == Id);
            if(!genre) { return NotFound(); }
            _dbContext.Genres.Remove(new Genre { Id = Id});
            
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
