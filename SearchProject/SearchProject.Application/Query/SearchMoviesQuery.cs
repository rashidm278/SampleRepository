using MediatR;
using SearchProject.Domain.Entities;
using SearchProject.Application.Dtos;


namespace SearchProject.Query
{
    public record SearchMoviesQuery(SearchRequest SearchRequest) : IRequest<MovieSearchResult>;

}
