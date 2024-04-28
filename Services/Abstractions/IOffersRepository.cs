using Models.ControllerModels;
using Models.ViewModels;

namespace Services.Abstractions
{
    public interface IOffersRepository
    {
        UserTenderOffersViewModel GetTenderOffers(UserTenderOffersModel model, Guid owner);
    }
}
