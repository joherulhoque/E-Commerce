using Microsoft.Data.SqlClient;
using SuperShop.Models;

namespace SuperShop.Services
{
    public class StaffService
    {
        private readonly string _connectionString;
        public StaffService(string connectionString) => _connectionString = connectionString;

        public void AddStaff(StaffUsers staff)
        {
            using var con = new SqlConnection(_connectionString);
            string query = "INSERT INTO StaffUser (ShopId, Name, Username, Password, Role, IsActive) VALUES (@ShopId,@Name,@Username,@Password,@Role,@IsActive)";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", staff.ShopId);
            cmd.Parameters.AddWithValue("@Name", staff.Name);
            cmd.Parameters.AddWithValue("@Username", staff.Username);
            cmd.Parameters.AddWithValue("@Password", staff.Password);
            cmd.Parameters.AddWithValue("@Role", staff.Role);
            cmd.Parameters.AddWithValue("@IsActive", staff.IsActive);
            con.Open(); cmd.ExecuteNonQuery();
        }

        public List<StaffUsers> GetStaff(int shopId)
        {
            var list = new List<StaffUsers>();
            using var con = new SqlConnection(_connectionString);
            string query = "SELECT * FROM StaffUser WHERE ShopId=@ShopId";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", shopId);
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StaffUsers
                {
                    StaffId = Convert.ToInt32(reader["StaffId"]),
                    ShopId = Convert.ToInt32(reader["ShopId"]),
                    Name = reader["Name"].ToString(),
                    Username = reader["Username"].ToString(),
                    Password = reader["Password"].ToString(),
                    Role = reader["Role"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                });
            }
            return list;
        }

        public void ToggleStaffStatus(int staffId, bool isActive)
        {
            using var con = new SqlConnection(_connectionString);
            string query = "UPDATE StaffUser SET IsActive=@IsActive WHERE StaffId=@StaffId";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@IsActive", isActive);
            cmd.Parameters.AddWithValue("@StaffId", staffId);
            con.Open(); cmd.ExecuteNonQuery();
        }
    }

}