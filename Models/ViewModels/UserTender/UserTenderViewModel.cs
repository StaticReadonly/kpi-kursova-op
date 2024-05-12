using Models.DbModels;

namespace Models.ViewModels.UserTender
{
    public class UserTenderViewModel : PaginationModelBase
    {
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();
    }
}