namespace Models
{
    public class TenderState
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TenderModel> Tenders { get; set; } = new List<TenderModel>();
    }
}
