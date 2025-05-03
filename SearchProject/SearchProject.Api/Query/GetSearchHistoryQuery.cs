using MediatR;
using SearchProject.Api.Domain;

namespace SearchProject.Api.Query
{
    public record GetSearchHistoryQuery(string Username) : IRequest<IEnumerable<SearchHistoryDto>>;
}
