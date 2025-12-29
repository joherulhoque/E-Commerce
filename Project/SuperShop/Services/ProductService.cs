using Microsoft.Data.SqlClient;
using SuperShop.Models;

namespace SuperShop.Services
{
    public class ProductService
    {
        private readonly string _connectionString;
        public ProductService(string connectionString) => _connectionString = connectionString;

        public void AddProduct(Products p)
        {
            using var con = new SqlConnection(_connectionString);
            string query = "INSERT INTO Products (ShopId, ProductName, SKU, SalePrice, Quantity) VALUES (@ShopId, @ProductName, @SKU, @Price, @Quantity)";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", p.ShopId);
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@SKU", p.SKU);
            cmd.Parameters.AddWithValue("@Price", p.SalePrice);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Products> GetProducts(int shopId)
        {
            var list = new List<Products>();
            using var con = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Products WHERE ShopId=@ShopId";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", shopId);
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Products
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ShopId = Convert.ToInt32(reader["ShopId"]),
                    ProductName = reader["ProductName"].ToString(),
                    SKU = reader["SKU"].ToString(),
                    SalePrice = Convert.ToDecimal(reader["SalePrice"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                });
            }
            return list;
        }
    }

}