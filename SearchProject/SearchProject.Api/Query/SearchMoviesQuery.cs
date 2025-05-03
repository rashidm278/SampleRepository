using SearchProject.Entities;
using MediatR;

namespace SearchProject.Query
{
    public record SearchMoviesQuery(string? searchQuery, string genre, string sortBy, int Page = 1, int PageSize = 10) : IRequest<MovieSearchResult>;

}
