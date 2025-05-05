using MediatR;
using SearchProject.Application.Dtos;

namespace SearchProject.Application.Query
{
    public record GetSearchHistoryQuery(string Username) : IRequest<IEnumerable<SearchHistoryDto>>;
}
