namespace SearchProject.Domain.Entities
{
    public class MovieSearchResult
    {
        public List<Movies> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
