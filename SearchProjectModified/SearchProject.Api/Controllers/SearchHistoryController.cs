using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchProject.Application.Query;

namespace SearchProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SearchHistoryController> _logger;

        /// <summary>
        /// search history constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public SearchHistoryController(IMediator mediator, ILogger<SearchHistoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// method to get user by name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var result = await _mediator.Send(new GetSearchHistoryQuery(username));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error");
                throw;
            }

        }
    }
}
