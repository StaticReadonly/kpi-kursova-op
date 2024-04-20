namespace Models.DbModels
{
    public class TenderStateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();
    }
}
