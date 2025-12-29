
using Microsoft.Data.SqlClient;
using SuperShop.Models;

namespace SuperShop.Services
{
    public class SubscriptionService
    {
        private readonly string _connectionString;

        public SubscriptionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Create Subscription
        public void CreateSubscription(Subscriptions sub)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Subscription (ShopId, StartDate, EndDate, Plans, IsActive)
                             VALUES (@ShopId, @StartDate, @EndDate, @Plan, @IsActive)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ShopId", sub.ShopId);
                cmd.Parameters.AddWithValue("@StartDate", sub.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", sub.EndDate);
                cmd.Parameters.AddWithValue("@Plan", sub.Plan);
                cmd.Parameters.AddWithValue("@IsActive", sub.IsActive);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Get subscriptions by shop
        public List<Subscriptions> GetSubscriptions(int shopId)
        {
            var list = new List<Subscriptions>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Subscription WHERE ShopId=@ShopId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ShopId", shopId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Subscriptions
                    {
                        SubscriptionId = Convert.ToInt32(reader["SubscriptionId"]),
                        ShopId = Convert.ToInt32(reader["ShopId"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        Plan = reader["Plans"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
            }
            return list;
        }

        // Enable/Disable Subscription
        public void ToggleSubscriptionStatus(int subscriptionId, bool isActive)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Subscription SET IsActive=@IsActive WHERE SubscriptionId=@SubscriptionId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@SubscriptionId", subscriptionId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}