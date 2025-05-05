using MediatR;
using SearchProject.Domain.Interfaces;
using SearchProject.Query;
using SearchProject.Domain.Entities;

namespace SearchProject.Handlers
{
    public class SearchMoviesHandler : IRequestHandler<SearchMoviesQuery, MovieSearchResult>
    {
        private readonly IMovieRepository _repository;

        public SearchMoviesHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovieSearchResult> Handle(SearchMoviesQuery queryRequest, CancellationToken cancellationToken)
        {
            return await _repository.SearchAsync(queryRequest.SearchRequest.Username, queryRequest.SearchRequest.SearchQuery,
                queryRequest.SearchRequest.Genre, queryRequest.SearchRequest.SortBy, queryRequest.SearchRequest.Page, queryRequest.SearchRequest.PageSize);
        }
    }
}
