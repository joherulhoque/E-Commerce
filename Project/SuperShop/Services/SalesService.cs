using Microsoft.Data.SqlClient;

namespace SuperShop.Models
{
    public class SalesService
    {
        private readonly string _connectionString;
        public SalesService(string connectionString) => _connectionString = connectionString;

        public void AddSale(Sale s)
        {
            using var con = new SqlConnection(_connectionString);
            string query = "INSERT INTO Sales (ShopId, ProductId, Quantity, UnitPrice) VALUES (@ShopId,@ProductId,@Quantity,@UnitPrice)";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", s.ShopId);
            cmd.Parameters.AddWithValue("@ProductId", s.ProductId);
            cmd.Parameters.AddWithValue("@Quantity", s.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", s.UnitPrice);
            con.Open(); cmd.ExecuteNonQuery();
        }

        public List<Sale> GetSales(int shopId)
        {
            var list = new List<Sale>();
            using var con = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Sales WHERE ShopId=@ShopId";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", shopId);
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Sale
                {
                    SaleId = Convert.ToInt32(reader["SaleId"]),
                    ShopId = Convert.ToInt32(reader["ShopId"]),
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    SaleDate = Convert.ToDateTime(reader["SaleDate"])
                });
            }
            return list;
        }
    }

}