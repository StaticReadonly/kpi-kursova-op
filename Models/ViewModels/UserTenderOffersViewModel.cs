using Models.DbModels;

namespace Models.ViewModels
{
    public class UserTenderOffersViewModel : PaginationModelBase
    {
        public Guid Id { get; set; }
        public List<UserTenderOfferViewModel> Offers { get; set; } = new List<UserTenderOfferViewModel>();
    }
}
