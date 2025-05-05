using MediatR;
using SearchProject.Application.Dtos;

namespace SearchProject.Application.Query
{
    public record GetAllErrorReportsQuery() : IRequest<IEnumerable<ErrorReportDto>>;
}
