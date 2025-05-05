namespace SearchProject.Application.Dtos
{
    public class SearchRequest
    {
        public SearchRequest() { }
        public string Username { get; set; } = null!;
        public string? SearchQuery { get; set; }
        public string? Genre { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
