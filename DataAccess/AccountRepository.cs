using BankManagementSystem.Domain;
using Microsoft.Data.SqlClient;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace BankManagementSystem.DataAccess
{
    public class AccountRepository
    {
        // Create new account
        public void Add(Account account)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Accounts (AccountNumber, CustomerId, AccountType, Balance) " +
                               "VALUES (@AccountNumber, @CustomerId, @AccountType, @Balance)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                    cmd.Parameters.AddWithValue("@CustomerId", account.CustomerId);
                    cmd.Parameters.AddWithValue("@AccountType", account.AccountType);
                    cmd.Parameters.AddWithValue("@Balance", account.Balance);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update account balance
        public void Update(Account account)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "UPDATE Accounts SET Balance=@Balance WHERE AccountNumber=@AccountNumber";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                    cmd.Parameters.AddWithValue("@Balance", account.Balance);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete account
        public void Delete(int accountNumber)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Accounts WHERE AccountNumber=@AccountNumber";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get account by account number
        public Account Get(int accountNumber)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Accounts WHERE AccountNumber=@AccountNumber";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Account account = new Account(
                                Convert.ToInt32(reader["CustomerId"]),
                                reader["AccountType"].ToString(),
                                Convert.ToDecimal(reader["Balance"])
                            );

                            account.AccountNumber = Convert.ToInt32(reader["AccountNumber"]);

                            return account;
                        }
                    }
                }
            }

            return null;
        }

        // Get all accounts
        public List<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Accounts";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Account account = new Account(
                                Convert.ToInt32(reader["CustomerId"]),
                                reader["AccountType"].ToString(),
                                Convert.ToDecimal(reader["Balance"])
                            );

                            account.AccountNumber = Convert.ToInt32(reader["AccountNumber"]);

                            accounts.Add(account);
                        }
                    }
                }
            }

            return accounts;
        }

        // Get next account number
        public int GetNextAccountNumber()
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT ISNULL(MAX(AccountNumber),0) + 1 FROM Accounts";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}