namespace SuperShop.Models
{
    public class Stocks
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalPurchase { get; set; }
        public int TotalSale { get; set; }
        public int CurrentStock => TotalPurchase - TotalSale;
    }

}