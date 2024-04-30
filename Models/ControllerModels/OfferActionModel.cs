namespace Models.ControllerModels
{
    public class OfferActionModel
    {
        public Guid TenderId { get; set; }
        public Guid OfferId { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
