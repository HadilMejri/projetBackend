using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace projnet
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void ConnectAndExecute()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful.");

                    // Execute database operations here

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to the database: " + ex.Message);
                }
            }
        }
    }
}
