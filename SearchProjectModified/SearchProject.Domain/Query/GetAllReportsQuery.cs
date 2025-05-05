using MediatR;
using SearchProject.Domain.Dtos;

namespace SearchProject.Domain.Query
{
    public record GetAllErrorReportsQuery() : IRequest<IEnumerable<ErrorReportDto>>;
}
