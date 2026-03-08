using Microsoft.Data.SqlClient;
using BankManagementSystem.DataAccess;
using System;

namespace BankManagementSystem
{
    public static class DbMigrator
    {
        public static void EnsureDatabaseSchema()
        {
            try
            {
                using (var conn = DbConnection.GetConnection())
                {
                    conn.Open();
                    string checkQuery = @"
                        IF NOT EXISTS (
                            SELECT * FROM sys.columns 
                            WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') 
                            AND name = 'Features'
                        )
                        BEGIN
                            ALTER TABLE Accounts ADD Features INT NOT NULL DEFAULT 0;
                        END";
                    using (var cmd = new SqlCommand(checkQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database setup error: {ex.Message}");
            }
        }
    }
}
