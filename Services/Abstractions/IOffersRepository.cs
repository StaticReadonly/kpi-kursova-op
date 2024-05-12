using Models.ControllerModels;
using Models.ViewModels.UserOffers;
using Models.ViewModels.UserTender;

namespace Services.Abstractions
{
    public interface IOffersRepository
    {
        UserTenderOffersViewModel GetTenderOffers(UserTenderOffersModel model, Guid owner);
        UserOffersViewModel GetUserOffers(UserOffersModel model, Guid userId);
        Task OfferAction(Guid guid, OfferActionModel model);
        Task NewOffer(NewOfferModel model);
    }
}
