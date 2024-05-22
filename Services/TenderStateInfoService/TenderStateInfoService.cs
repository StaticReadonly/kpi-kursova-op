using Models.DbModels;
using Services.Abstractions;
using System.Runtime.CompilerServices;

namespace Services.TenderStateInfoService
{
    public class TenderStateInfoService : ITenderStateInfoService
    {
        public bool IsAcceptedOffer(TenderModel model)
        {
            return model.StateId == 2;
        }

        public bool IsCreated(TenderModel model)
        {
            return model.StateId == 3;
        }

        public bool IsWaitingForOffers(TenderModel model)
        {
            return model.StateId == 1;
        }
    }
}
