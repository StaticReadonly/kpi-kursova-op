namespace Models.DbModels
{
    public class OfferModel
    {
        public Guid Id { get; set; }
        public Guid TenderId { get; set; }
        public Guid OffererId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public int StateId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
