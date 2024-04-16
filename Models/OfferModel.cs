namespace Models
{
    public class OfferModel
    {
        public Guid Id { get; set; }
        public Guid TenderId { get; set; }
        public Guid OffererId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
