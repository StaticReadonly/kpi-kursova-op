using Models.DbModels;

namespace Services.TenderStateInfoService
{
    public static class TenderStateInfoServiceExtentions
    {
        public static IQueryable<TenderModel> WhereIsWaitingForOffers(this IQueryable<TenderModel> query)
        {
            return query.Where(t => t.StateId == 1);
        }

        public static IQueryable<TenderModel> IsAcceptedOffer(this IQueryable<TenderModel> query)
        {
            return query.Where(t => t.StateId == 2);
        }

        public static IQueryable<TenderModel> IsCreated(this IQueryable<TenderModel> query)
        {
            return query.Where(t => t.StateId == 3);
        }
    }
}
