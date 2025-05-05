using System.ComponentModel.DataAnnotations;

namespace SearchProject.Domain.Entities
{
    public class ErrorReport
    {
        [Key]
        public int ErrorId { get; set; }

        [MaxLength(255)]
        public string Username { get; set; } = null!;

        [MaxLength(500)]
        public string Message { get; set; } = null!;

        [MaxLength(255)]
        public string? StackTrace { get; set; }

        [MaxLength(255)]
        public string? Url { get; set; }
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    }
}
