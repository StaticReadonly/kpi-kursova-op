namespace Models
{
    public class OrganisationModel
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Website { get; set; } = null;
        public List<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
