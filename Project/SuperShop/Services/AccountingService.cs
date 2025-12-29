using Microsoft.Data.SqlClient;
using SuperShop.Models;

namespace SuperShop.Services
{
    public class AccountingService
    {
        private readonly string _connectionString;
        public AccountingService(string connectionString) => _connectionString = connectionString;

        // Cash Entry
        public void AddCash(CashEntry entry)
        {
            using var con = new SqlConnection(_connectionString);
            string query = "INSERT INTO Cash (ShopId, Date, Amount, Type, Description) VALUES (@ShopId,@Date,@Amount,@Type,@Description)";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", entry.ShopId);
            cmd.Parameters.AddWithValue("@Date", entry.Date);
            cmd.Parameters.AddWithValue("@Amount", entry.Amount);
            cmd.Parameters.AddWithValue("@Type", entry.Type);
            cmd.Parameters.AddWithValue("@Description", entry.Description);
            con.Open(); cmd.ExecuteNonQuery();
        }

        public List<CashEntry> GetCashEntries(int shopId)
        {
            var list = new List<CashEntry>();
            using var con = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Cash WHERE ShopId=@ShopId ORDER BY Date DESC";
            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ShopId", shopId);
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CashEntry
                {
                    CashId = Convert.ToInt32(reader["CashId"]),
                    ShopId = Convert.ToInt32(reader["ShopId"]),
                    Date = Convert.ToDateTime(reader["Date"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    Type = reader["Type"].ToString(),
                    Description = reader["Description"].ToString()
                });
            }
            return list;
        }
    }

}