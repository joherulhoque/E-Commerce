namespace SuperShop.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal UnitPrice { get; set; }
    }

}