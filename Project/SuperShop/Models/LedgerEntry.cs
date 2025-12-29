
namespace SuperShop.Models
{
    public class LedgerEntry
    {
        public int LedgerId { get; set; }
        public int ShopId { get; set; }
        public DateTime Date { get; set; }
        public string AccountName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Description { get; set; }
    }

}