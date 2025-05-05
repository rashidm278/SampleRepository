using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SearchProject.Application.Command;
using SearchProject.Controllers;
using SearchProject.Domain.Entities;
using SearchProject.Application.Dtos;
using SearchProject.Query;
using System.ComponentModel.DataAnnotations;


namespace SearchProject.Tests.Controllers
{
    [TestFixture]
    public class MovieControllerTests
    {
        private Mock<ILogger<MovieController>> _loggerMock;
        private Mock<IMediator> _mediatorMock;
        private MovieController _controller;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<MovieController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new MovieController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Test]
        public async Task Search_ReturnsOkResult_WhenSuccessful()
        {
            var request = new SearchRequest();
            var movies = new List<Movies> { new Movies { Title = "Test" } };
            var movieSearchResult = new MovieSearchResult()
            {
                Items = movies,
                Page = 1,
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<SearchMoviesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(movieSearchResult);

            var result = await _controller.Search(request);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(movieSearchResult, okResult.Value);
        }

        [Test]
        public async Task Search_ReturnsBadRequest_OnValidationException()
        {
            var request = new SearchRequest();
            _mediatorMock.Setup(m => m.Send(It.IsAny<SearchMoviesQuery>(), default))
                         .ThrowsAsync(new ValidationException("Invalid search"));

            var result = await _controller.Search(request);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequest = (BadRequestObjectResult)result;
            StringAssert.Contains("Invalid search", badRequest.Value.ToString());
        }

        [Test]
        public void Search_Throws_OnUnhandledException()
        {
            var request = new SearchRequest();
            _mediatorMock.Setup(m => m.Send(It.IsAny<SearchMoviesQuery>(), default))
                         .ThrowsAsync(new Exception("Unhandled"));

            var ex = Assert.ThrowsAsync<Exception>(() => _controller.Search(request));
            Assert.AreEqual("Unhandled", ex.Message);
        }

        // ---------- Create Tests ----------

        [Test]
        public async Task Create_ReturnsCreatedAtResult_WhenSuccessful()
        {
            var command = new CreateMovieCommand() { Title = "m1"};
            var movieDto = new Movies { MovieId = 1, Title = "Test" };


            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(movieDto);

            var result = await _controller.Create(command);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAt = (CreatedAtActionResult)result;
            Assert.AreEqual("GetById", createdAt.ActionName);
            Assert.AreEqual(movieDto.MovieId, ((Movies)createdAt.Value).MovieId);
        }

        [Test]
        public async Task Create_ReturnsBadRequest_OnValidationException()
        {
            var command = new CreateMovieCommand();

            _mediatorMock.Setup(m => m.Send(command, default))
                         .ThrowsAsync(new ValidationException("Validation failed"));

            var result = await _controller.Create(command);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequest = (BadRequestObjectResult)result;
            StringAssert.Contains("Validation failed", badRequest.Value.ToString());
        }

        [Test]
        public void Create_Throws_OnUnhandledException()
        {
            var command = new CreateMovieCommand();

            _mediatorMock.Setup(m => m.Send(command, default))
                         .ThrowsAsync(new Exception("Create failed"));

            var ex = Assert.ThrowsAsync<Exception>(() => _controller.Create(command));
            Assert.AreEqual("Create failed", ex.Message);
        }

        [Test]
        public async Task GetById_ReturnsOk()
        {
            var result = await _controller.GetById(1);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}