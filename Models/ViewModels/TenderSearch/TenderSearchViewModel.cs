using Models.DbModels;

namespace Models.ViewModels.TenderSearch
{
    public class TenderSearchViewModel : PaginationModelBase
    {
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();
    }
}
