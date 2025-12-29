using Microsoft.Data.SqlClient;

namespace SuperShop.Models
{
    public class StockService
    {
        private readonly string _connectionString;
        public StockService(string connectionString) => _connectionString = connectionString;

        public List<Stocks> GetStock(int shopId)
        {
            var list = new List<Stocks>();
            using var con = new SqlConnection(_connectionString);
            string query = @"
        SELECT p.ProductId, p.ProductName,
        ISNULL((SELECT SUM(Quantity) FROM Purchase WHERE ProductId=p.ProductId AND ShopId=@ShopId),0) as TotalPurchase,
        ISNULL((SELECT SUM(Quantity) FROM Sales WHERE ProductId=p.ProductId AND ShopId=@ShopId),0) as TotalSale
        FROM Products p
        WHERE p.ShopId=@ShopId";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", shopId);
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Stocks
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = reader["ProductName"].ToString(),
                    TotalPurchase = Convert.ToInt32(reader["TotalPurchase"]),
                    TotalSale = Convert.ToInt32(reader["TotalSale"])
                });
            }
            return list;
        }
    }

}