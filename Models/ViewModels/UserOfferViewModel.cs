namespace Models.ViewModels
{
    public class UserOfferViewModel
    {
        public string TenderName { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public string State { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
