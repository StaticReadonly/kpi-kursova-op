namespace Models.ViewModels.UserTender
{
    public class UserTenderOfferViewModel
    {
        public Guid Id { get; set; }
        public string OffererName { get; set; } = string.Empty;
        public string OffererSurname { get; set; } = string.Empty;
        public string OffererPatronimyc { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int StateId { get; set; }
        public string State { get; set; } = string.Empty;
    }
}
