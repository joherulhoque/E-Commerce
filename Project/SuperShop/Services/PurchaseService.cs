using Microsoft.Data.SqlClient;
using SuperShop.Models;

namespace SuperShop.Services
{
    public class PurchaseService
    {
        private readonly string _connectionString;
        public PurchaseService(string connectionString) => _connectionString = connectionString;

        public void AddPurchase(Purchases p)
        {
            using var con = new SqlConnection(_connectionString);
            string query = "INSERT INTO PurchaseMaster (ShopId, ProductId, Quantity, UnitPrice) VALUES (@ShopId,@ProductId,@Quantity,@UnitPrice)";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", p.ShopId);
            cmd.Parameters.AddWithValue("@ProductId", p.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", p.UnitPrice);
            con.Open(); cmd.ExecuteNonQuery();
        }

        public List<Purchases> GetPurchases(int shopId)
        {
            var list = new List<Purchases>();
            using var con = new SqlConnection(_connectionString);
            string query = "SELECT * FROM PurchaseMaster WHERE ShopId=@ShopId";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", shopId);
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Purchases
                {
                    PurchaseId = Convert.ToInt32(reader["PurchaseId"]),
                    ShopId = Convert.ToInt32(reader["ShopId"]),
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"])
                });
            }
            return list;
        }
    }

}