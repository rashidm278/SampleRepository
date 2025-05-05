using Moq;
using SearchProject.Domain.Dtos;
using SearchProject.Domain.Entities;
using SearchProject.Domain.Interfaces;
using SearchProject.Domain.Query;
using SearchProject.Handlers;

namespace SearchProject.Tests.Handlers
{
    [TestFixture]
    public class SearchMoviesHandlerTests
    {
        private Mock<IMovieRepository> _movieRepositoryMock;
        private SearchMoviesHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _handler = new SearchMoviesHandler(_movieRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var request = new SearchRequest
            {
                Username = "john",
                SearchQuery = "Matrix",
                Genre = "Sci-Fi",
                SortBy = "rating",
                Page = 1,
                PageSize = 10
            };

            var expectedResult = new MovieSearchResult
            {
                Items = new List<Movies> { new Movies { Title = "The Matrix" } },
                TotalCount = 1
            };

            _movieRepositoryMock
                .Setup(repo => repo.SearchAsync(request.Username, request.SearchQuery, request.Genre, request.SortBy, request.Page, request.PageSize))
                .ReturnsAsync(expectedResult);

            var query = new SearchMoviesQuery(request);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedResult, result);
            _movieRepositoryMock.Verify(repo =>
                repo.SearchAsync(
                    request.Username,
                    request.SearchQuery,
                    request.Genre,
                    request.SortBy,
                    request.Page,
                    request.PageSize),
                Times.Once);
        }
    }
}