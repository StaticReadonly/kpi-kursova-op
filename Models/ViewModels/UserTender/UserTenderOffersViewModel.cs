namespace Models.ViewModels.UserTender
{
    public class UserTenderOffersViewModel : PaginationModelBase
    {
        public Guid Id { get; set; }
        public List<UserTenderOfferViewModel> Offers { get; set; } = new List<UserTenderOfferViewModel>();
    }
}
