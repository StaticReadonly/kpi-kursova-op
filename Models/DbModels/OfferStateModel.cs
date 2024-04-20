namespace Models.DbModels
{
    public class OfferStateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<OfferModel> Offers { get; set; } = new List<OfferModel>();
    }
}
