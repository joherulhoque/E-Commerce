namespace SuperShop.Models
{

    public class BankEntry
    {
        public int BankId { get; set; }
        public int ShopId { get; set; }
        public string BankName { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // In / Out
        public string Description { get; set; }
    }
}