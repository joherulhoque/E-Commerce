namespace SuperShop.Models
{
    public class Subscriptions
    {
        public int SubscriptionId { get; set; }
        public int ShopId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Plan { get; set; }
        public bool IsActive { get; set; }
    }
}