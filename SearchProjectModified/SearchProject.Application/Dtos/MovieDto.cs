namespace SearchProject.Application.Dtos
{
    public class MovieDto
    {
        /// <summary>
        /// unique id of the movie
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// title of the movie
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// title of the movie
        /// </summary>

        public string Content { get; set; }

        /// <summary>
        /// title of the movie
        /// </summary>

        public string Genre { get; set; }

        /// <summary>
        /// Rating of the movie
        /// </summary>

        public string Rating { get; set; }

        /// <summary>
        /// Description of the movie
        /// </summary>

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
