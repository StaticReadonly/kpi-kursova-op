namespace Models.ControllerModels
{
    public class NewOfferModel
    {
        public Guid TenderId { get; set; }
        public Guid OwnerId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
