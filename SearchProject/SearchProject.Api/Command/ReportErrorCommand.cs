using MediatR;
using SearchProject.Command;
using SearchProject.Entities;

namespace SearchProject.Api.Command
{
    public class ReportErrorCommand : IRequest<ErrorReport>
    {
        public string Username { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string StackTrace { get; set; } = null!;
        public string Url { get; set; } = null!;


    }
}
