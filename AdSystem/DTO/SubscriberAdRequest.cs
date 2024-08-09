namespace AdSystem.DTO
{
    public class SubscriberAdRequest
    {
        public int SubscriptionNumber { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal ItemPrice { get; set; }
    }
}