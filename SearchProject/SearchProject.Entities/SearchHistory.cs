using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchProject.Entities
{
    /// <summary>
    /// Entity for search history
    /// </summary>
    public class SearchHistory : BaseEntity
    {
        /// <summary>
        /// Search history Id
        /// </summary>
        [Key]
        public int SearchHistoryId { get; set; }

        /// <summary>
        /// Search query
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string SearchQuery { get; set; }

        /// <summary>
        /// search date
        /// </summary>
        [Required]
        public DateTime SearchedDate { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        [Required]
        public int UserId {  get; set; }

        /// <summary>
        /// user entity
        /// </summary>  
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
