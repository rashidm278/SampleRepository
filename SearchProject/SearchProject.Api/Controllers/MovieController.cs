using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchProject.Api.Command;
using SearchProject.Query;
using System.ComponentModel.DataAnnotations;

namespace SearchProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// movie controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public MovieController(ILogger<MovieController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }     
        
        
        /// <summary>
        /// method to search from movie
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="genre"></param>
        /// <param name="sortBy"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string searchQuery, [FromQuery] string genre, [FromQuery] string sortBy, [FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var result = await _mediator.Send(new SearchMoviesQuery(searchQuery, genre, sortBy, page, pageSize));
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Unexpected Error");
                throw;
            }
        }



        /// <summary>
        /// method to create movies
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateMovieCommand command)
        {
            try
            {
                var movie = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = movie.MovieId }, movie);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error");
                throw;
            }
        }


        /// <summary>
        /// method to get movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            // TODO: Implement GetById logic via repository
            return Ok();
        }
    }
}
