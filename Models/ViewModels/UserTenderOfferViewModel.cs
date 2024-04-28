namespace Models.ViewModels
{
    public class UserTenderOfferViewModel
    {
        public Guid Id { get; set; }
        public string OffererName { get; set; }
        public string OffererSurname { get; set; }
        public string OffererPatronimyc { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
