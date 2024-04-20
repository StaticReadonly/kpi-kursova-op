namespace Models.ViewModels
{
    public class PaginationModelBase
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Query { get; set; } = null;
    }
}
