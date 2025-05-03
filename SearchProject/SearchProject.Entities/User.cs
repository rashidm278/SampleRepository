using System.ComponentModel.DataAnnotations;

namespace SearchProject.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// unique user id
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// email of the user
        /// </summary>
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Store hashed password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// store refresh token
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// store refresh token expiry
        /// </summary>
        public DateTime? RefreshTokenExpiry { get; set; }

        /// <summary>
        /// role of the user
        /// </summary>
        [Required]
        public UserRole Role { get; set; }

        /// <summary>
        /// search histoty collection
        /// </summary>
        public ICollection<SearchHistory> SearchHistories { get; set; }

    }

    public enum UserRole
    {
        Admin,
        Operator,
        Member
    }
}
