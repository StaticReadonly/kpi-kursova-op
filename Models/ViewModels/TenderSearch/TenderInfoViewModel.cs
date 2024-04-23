using Models.DbModels;

namespace Models.ViewModels.TenderSearch
{
    public class TenderInfoViewModel
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string OwnerFullName { get; set; } = string.Empty;
        public string? ExecuterFullName { get; set; } = null;
        public DateTime CreationDate { get; set; }
        public List<OfferModel> Offers { get; set; }
    }
}
