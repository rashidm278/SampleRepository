using MediatR;
using SearchProject.Api.Domain;

namespace SearchProject.Api.Query
{
    public record GetAllErrorReportsQuery() : IRequest<List<ErrorReportDto>>;
}
