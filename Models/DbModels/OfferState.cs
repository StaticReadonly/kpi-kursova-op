namespace Models.DbModels
{
    public class OfferState
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<OfferModel> Offers { get; set; } = new List<OfferModel>();
    }
}
