using Microsoft.Data.SqlClient;
using SuperShop.Models;

namespace SuperShop.Services
{
    public class POSService
    {
        private readonly string _connectionString;
        public POSService(string connectionString) => _connectionString = connectionString;

        // Create Invoice with Details
        public void CreateInvoice(Invoice invoice)
        {
            using var con = new SqlConnection(_connectionString);
            con.Open();
            using var transaction = con.BeginTransaction();
            try
            {
                // Insert Invoice
                string queryInvoice = "INSERT INTO Invoice (ShopId, StaffId, TotalAmount, PaymentType) OUTPUT INSERTED.InvoiceId VALUES (@ShopId,@StaffId,@TotalAmount,@PaymentType)";
                using var cmd = new SqlCommand(queryInvoice, con, transaction);
                cmd.Parameters.AddWithValue("@ShopId", invoice.ShopId);
                cmd.Parameters.AddWithValue("@StaffId", invoice.StaffId);
                cmd.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                cmd.Parameters.AddWithValue("@PaymentType", invoice.PaymentType);
                int invoiceId = (int)cmd.ExecuteScalar();

                // Insert Details
                foreach (var detail in invoice.Details)
                {
                    string queryDetail = "INSERT INTO InvoiceDetails (InvoiceId, ProductId, Quantity, UnitPrice) VALUES (@InvoiceId,@ProductId,@Quantity,@UnitPrice)";
                    using var cmdDetail = new SqlCommand(queryDetail, con, transaction);
                    cmdDetail.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    cmdDetail.Parameters.AddWithValue("@ProductId", detail.ProductId);
                    cmdDetail.Parameters.AddWithValue("@Quantity", detail.Quantity);
                    cmdDetail.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
                    cmdDetail.ExecuteNonQuery();

                    // Update Product Stock
                    string queryStock = "UPDATE Products SET Quantity = Quantity - @Qty WHERE ProductId=@ProductId";
                    using var cmdStock = new SqlCommand(queryStock, con, transaction);
                    cmdStock.Parameters.AddWithValue("@Qty", detail.Quantity);
                    cmdStock.Parameters.AddWithValue("@ProductId", detail.ProductId);
                    cmdStock.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

}