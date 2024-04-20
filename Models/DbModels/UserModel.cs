namespace Models.DbModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Patronimyc { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<TenderModel> Tenders { get; set; }
        public List<TenderModel> CompletedTenders { get; set; }
        public List<OfferModel> Offers { get; set; }
    }
}
