using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SearchProject.Api.Command;
using SearchProject.Controllers;
using SearchProject.Query;
using SearchProject.Entities;

namespace SearchProject.Tests
{
    [TestFixture]
    public class MovieControllerTests
    {
        private MovieController _controller;
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<MovieController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<MovieController>>();
            _controller = new MovieController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Test]
        public async Task Search_ReturnsOk_WithResults()
        {
            // Arrange
            var movies = new List<Movies> {
                new Movies { MovieId = 1, Title = "Test Movie", Genre = "Action" }
            };

            var expectedResults = new MovieSearchResult() { Items = movies, TotalCount = 100, PageSize = 10, Page = 1 };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SearchMoviesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResults);

            // Act
            var result = await _controller.Search("query", "genre", "sort", 1, 10) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedResults, result.Value);
        }

        [Test]
        public async Task Create_ReturnsCreated_WithMovie()
        {
            // Arrange
            var createCommand = new CreateMovieCommand { Title = "Movie A" };
            var createdMovie = new Movies { MovieId = 1, Title = "Movie A" };

            _mediatorMock
                .Setup(m => m.Send(createCommand, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdMovie);

            // Act
            var result = await _controller.Create(createCommand) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("GetById", result.ActionName);
            Assert.AreEqual(createdMovie, result.Value);
        }
    }
}