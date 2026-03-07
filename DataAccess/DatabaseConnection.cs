using System;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace BankManagementSystem.DataAccess
{
    public static class DbConnection
    {
        private static string connectionString = "Data Source=localhost;Initial Catalog=BankManagementSystemDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}