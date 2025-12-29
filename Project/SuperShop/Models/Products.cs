namespace SuperShop.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}