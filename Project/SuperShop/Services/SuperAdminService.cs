using Microsoft.Data.SqlClient;
using SuperShop.Models;

namespace SuperShop.Services
{
    public class SuperAdminService
    {

        private readonly string _connectionString;

        public SuperAdminService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Shop Create
        public void CreateShop(Shops shop)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Shops (ShopName, OwnerName) VALUES (@ShopName, @OwnerName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ShopName", shop.ShopName);
                cmd.Parameters.AddWithValue("@OwnerName", shop.OwnerName);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Get all shops
        public List<Shops> GetShops()
        {
            var list = new List<Shops>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Shops";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Shops
                    {
                        ShopId = Convert.ToInt32(reader["ShopId"]),
                        ShopName = reader["ShopName"].ToString(),
                        OwnerName = reader["OwnerName"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    });
                }
            }
            return list;
        }

        // Enable / Disable shop
        public void ToggleShopStatus(int shopId, bool isActive)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Shops SET IsActive=@IsActive WHERE ShopId=@ShopId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@ShopId", shopId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}

