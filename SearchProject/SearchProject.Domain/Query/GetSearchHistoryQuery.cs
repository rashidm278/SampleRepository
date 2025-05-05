using MediatR;
using SearchProject.Domain.Dtos;

namespace SearchProject.Domain.Query
{
    public record GetSearchHistoryQuery(string Username) : IRequest<IEnumerable<SearchHistoryDto>>;
}
