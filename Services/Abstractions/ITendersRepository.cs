using Models.ControllerModels;
using Models.DbModels;
using Models.ViewModels.TenderSearch;
using Models.ViewModels.UserTender;

namespace Services.Abstractions
{
    public interface ITendersRepository
    {
        TenderSearchViewModel GetTenders(TenderSearchModel searchModel, Guid? userId);

        TenderModel GetTenderInfo(Guid Id, Guid? userId);

        UserTenderViewModel GetUserTenders(TenderSearchModel model, Guid userId);

        Task CreateNewTender(NewTenderModel model, Guid user);

        Task TenderInitialAction(TenderInitialActionModel model, Guid user);
    }
}