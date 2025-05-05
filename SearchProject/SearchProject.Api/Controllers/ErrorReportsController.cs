using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchProject.Domain.Command;
using SearchProject.Domain.Query;
using System.ComponentModel.DataAnnotations;

namespace SearchProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ErrorReportsController> _logger;

        /// <summary>
        /// eroor report controller constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ErrorReportsController(IMediator mediator, ILogger<ErrorReportsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// method to report error
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Report([FromBody] ReportErrorCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAll), new { id = result.ErrorId }, result);
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
        /// method to get all erros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetAllErrorReportsQuery());
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
