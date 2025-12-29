namespace SuperShop.Models
{

    public class CashEntry
    {
        public int CashId { get; set; }
        public int ShopId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // In / Out
        public string Description { get; set; }
    }
}