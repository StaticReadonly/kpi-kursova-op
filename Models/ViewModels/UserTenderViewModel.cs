using Models.DbModels;

namespace Models.ViewModels
{
    public class UserTenderViewModel : PaginationModelBase
    {
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();
    }
}