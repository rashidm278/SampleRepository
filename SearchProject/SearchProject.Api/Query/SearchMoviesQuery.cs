using SearchProject.Entities;
using MediatR;
using SearchProject.Api.Domain;

namespace SearchProject.Query
{
    public record SearchMoviesQuery(SearchRequest SearchRequest) : IRequest<MovieSearchResult>;

}
