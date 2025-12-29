using Microsoft.Data.SqlClient;

namespace SuperShop.Services
{
    public class UserService
    {
        private readonly string _connectionString;
        public string Role { get; private set; }
        public int ShopId { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Role);

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool Login(string username, string password)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            string query = "SELECT Role, ShopId FROM UserLogin WHERE Username=@Username AND Password=@Password";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            con.Open();
            using SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Role = dr["Role"].ToString();
                ShopId = dr["ShopId"] != DBNull.Value ? Convert.ToInt32(dr["ShopId"]) : 0;
                return true;
            }
            else
            {
                Role = null;
                ShopId = 0;
                return false;
            }
        }

        public void Logout()
        {
            Role = null;
            ShopId = 0;

        }
    }



}
