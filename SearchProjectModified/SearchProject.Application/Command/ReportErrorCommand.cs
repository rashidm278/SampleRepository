using MediatR;
using SearchProject.Domain.Entities;

namespace SearchProject.Application.Command
{
    public class ReportErrorCommand : IRequest<ErrorReport>
    {
        public string Username { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string StackTrace { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
