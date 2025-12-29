namespace SuperShop.Models
{
    public class Shops
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string OwnerName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
