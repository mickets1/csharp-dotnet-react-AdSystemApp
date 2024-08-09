namespace AdSystem.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public decimal ItemPrice { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public decimal AdPrice { get; set; }
        public bool IsSubscriber { get; set; }
        public int AdvertiserId { get; set; } // Foreign key to Advertiser
        public Advertiser Advertiser { get; set; } // Reference to Advertiser
    }
}
