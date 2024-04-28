using Models.ControllerModels;
using Models.DbModels;
using Models.ViewModels;

namespace Services.Abstractions
{
    public interface ITendersRepository
    {
        TenderSearchViewModel GetTenders(TenderSearchModel searchModel);

        TenderModel GetTenderInfo(Guid Id);

        UserTenderViewModel GetUserTenders(TenderSearchModel model, Guid userId);
    }
}