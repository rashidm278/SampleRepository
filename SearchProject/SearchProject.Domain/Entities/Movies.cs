using System.ComponentModel.DataAnnotations;

namespace SearchProject.Domain.Entities
{
    /// <summary>
    /// Movie Entity
    /// </summary>
    public class Movies : BaseEntity
    {
        /// <summary>
        /// unique id of the movie
        /// </summary>
        [Key]
        public int MovieId { get; set; }

        /// <summary>
        /// title of the movie
        /// </summary>
        [MaxLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// title of the movie
        /// </summary>
        [MaxLength(255)]

        public string Content { get; set; }

        /// <summary>
        /// title of the movie
        /// </summary>
        [MaxLength(255)]

        public string Genre { get; set; }

        /// <summary>
        /// Rating of the movie
        /// </summary>
        [MaxLength(255)]

        public string Rating { get; set; }

        /// <summary>
        /// Description of the movie
        /// </summary>
        [MaxLength(500)]

        public string Description { get; set; }

        /// <summary>
        /// Published Date of the movie
        /// </summary>
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// Popularity of the movie
        /// </summary>
        public int Popularity { get; set; }
    }
}
