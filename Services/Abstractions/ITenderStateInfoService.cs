using Models.DbModels;

namespace Services.Abstractions
{
    public interface ITenderStateInfoService
    {
        public bool IsWaitingForOffers(TenderModel model);
        public bool IsAcceptedOffer(TenderModel model);
        public bool IsCreated(TenderModel model);
    }
}
