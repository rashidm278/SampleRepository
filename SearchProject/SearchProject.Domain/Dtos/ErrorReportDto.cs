namespace SearchProject.Domain.Dtos
{
    public class ErrorReportDto
    {
        public int ErrorId { get; set; }

        public string Username { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string? StackTrace { get; set; }

        public string? Url { get; set; }
        public DateTime ReportedAt { get; set; }
    }
}
