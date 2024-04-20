namespace Models.ViewModels
{
    public class UserOffersViewModel : PaginationModelBase
    {
        public List<UserOfferViewModel> Offers { get; set; } = new List<UserOfferViewModel>();
    }
}
