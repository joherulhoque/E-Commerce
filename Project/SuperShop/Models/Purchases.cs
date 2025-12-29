namespace SuperShop.Models
{
    public class Purchases
    {
        public int PurchaseId { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal UnitPrice { get; set; }
    }

}