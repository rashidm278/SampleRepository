using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchProject.Domain.Command;
using SearchProject.Domain.Dtos;
using SearchProject.Domain.Query;
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
        [HttpGet("Search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery]SearchRequest searchRequest)
        {
            try
            {
                var result = await _mediator.Send(new SearchMoviesQuery(searchRequest));
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
        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateMovieCommand command)
        {
            try
            {
                var movie = await _mediator.Send(command);
                return Ok(movie);
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
    }
}
