namespace SuperShop.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int ShopId { get; set; }
        public int StaffId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; }
        public List<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();
    }
}