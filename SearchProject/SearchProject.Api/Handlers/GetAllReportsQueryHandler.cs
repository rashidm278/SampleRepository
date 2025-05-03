using MediatR;
using SearchProject.Api.Query;
using SearchProject.Entities;
using SearchProject.Interfaces.Interfaces;
using SearchProject.Api.Domain;

namespace SearchProject.Api.Handlers
{
    public class GetAllErrorReportsQueryHandler : IRequestHandler<GetAllErrorReportsQuery, IEnumerable<ErrorReportDto>>
    {
        private readonly IErrorReportRepository _repository;

        public GetAllErrorReportsQueryHandler(IErrorReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ErrorReportDto>> Handle(GetAllErrorReportsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAllAsync();
            var errorReports = entity.Select(e => new ErrorReportDto
            {
                ErrorId = e.ErrorId,
                Username = e.Username,
                Message = e.Message,
                StackTrace = e.StackTrace,
                Url = e.Url,
                ReportedAt = e.ReportedAt

            });
            return errorReports;
        }
    }
}
