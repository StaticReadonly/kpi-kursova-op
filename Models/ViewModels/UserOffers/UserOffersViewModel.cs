using Models.DbModels;

namespace Models.ViewModels.UserOffers
{
    public class UserOffersViewModel : PaginationModelBase
    {
        public List<OfferModel> Offers { get; set; } = new List<OfferModel>();
    }
}
