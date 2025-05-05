namespace SearchProject.Domain.Dtos
{
    public class SearchHistoryDto
    {
        /// <summary>
        /// Search history Id
        /// </summary>
        public int SearchHistoryId { get; set; }

        /// <summary>
        /// Search query
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// search date
        /// </summary>
        public DateTime SearchedDate { get; set; }
    }
}
