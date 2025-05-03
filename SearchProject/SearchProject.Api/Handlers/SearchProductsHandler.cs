using SearchProject.Entities;
using MediatR;
using SearchProject.Interfaces;
using SearchProject.Query;

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
            return await _repository.SearchAsync(queryRequest.searchQuery, queryRequest.genre, queryRequest.sortBy, queryRequest.Page, queryRequest.PageSize);
        }
    }
}
