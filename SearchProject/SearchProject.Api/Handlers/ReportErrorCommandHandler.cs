using MediatR;
using SearchProject.Api.Command;
using SearchProject.Entities;
using SearchProject.Interfaces.Interfaces;

namespace SearchProject.Api.Handlers
{
    public class ReportErrorCommandHandler : IRequestHandler<ReportErrorCommand, ErrorReport>
    {
        private readonly IErrorReportRepository _repository;

        public ReportErrorCommandHandler(IErrorReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorReport> Handle(ReportErrorCommand request, CancellationToken cancellationToken)
        {
            var report = new ErrorReport
            {
                Username = request.Username,
                Message = request.Message,
                StackTrace = request.StackTrace,
                Url = request.Url,
                ReportedAt = DateTime.UtcNow
            };

            return await _repository.AddAsync(report);
        }
    }
}
