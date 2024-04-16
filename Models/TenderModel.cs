namespace Models
{
    public class TenderModel
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime? SubscriptionTime { get; set; } = null;
        public DateTime? StartTime { get; set;} = null;
        public DateTime? EndTime { get; set;} = null;
        public Guid OwnerId { get; set; }
        public Guid? ExecuterId { get; set; } = null;
        public List<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
