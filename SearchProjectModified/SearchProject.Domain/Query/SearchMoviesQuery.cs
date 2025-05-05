using MediatR;
using SearchProject.Domain.Entities;
using SearchProject.Domain.Dtos;


namespace SearchProject.Domain.Query
{
    public record SearchMoviesQuery(SearchRequest SearchRequest) : IRequest<MovieSearchResult>;

}
