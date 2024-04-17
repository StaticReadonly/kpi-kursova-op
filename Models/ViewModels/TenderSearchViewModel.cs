using Models.DbModels;

namespace Models.ViewModels
{
    public class TenderSearchViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Query { get; set; } = null;
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();
    }
}
