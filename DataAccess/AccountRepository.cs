using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using BankManagementSystem.Domain;
using Microsoft.Data.SqlClient;

namespace BankManagementSystem.DataAccess
{
    public class AccountRepository : IRepository<Account>
    {
        // Create new account
        public void Add(Account account)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Accounts (AccountNumber, CustomerId, AccountType, Balance, Features) " +
                               "VALUES (@AccountNumber, @CustomerId, @AccountType, @Balance, @Features)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                    cmd.Parameters.AddWithValue("@CustomerId", account.CustomerId);
                    cmd.Parameters.AddWithValue("@AccountType", account.AccountType);
                    cmd.Parameters.AddWithValue("@Balance", account.Balance);
                    cmd.Parameters.AddWithValue("@Features", (int)account.Features);

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

                string query = "UPDATE Accounts SET Balance=@Balance, Features=@Features WHERE AccountNumber=@AccountNumber";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
                    cmd.Parameters.AddWithValue("@Balance", account.Balance);
                    cmd.Parameters.AddWithValue("@Features", (int)account.Features);

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
                            string type = reader["AccountType"].ToString();
                            int custId = Convert.ToInt32(reader["CustomerId"]);
                            decimal bal = Convert.ToDecimal(reader["Balance"]);
                            Account account;

                            if (type.Equals("Savings", StringComparison.OrdinalIgnoreCase))
                                account = new SavingsAccount(custId, bal);
                            else if (type.Equals("Checking", StringComparison.OrdinalIgnoreCase))
                                account = new CheckingAccount(custId, bal);
                            else
                                account = new Account(custId, type, bal);

                            account.AccountNumber = Convert.ToInt32(reader["AccountNumber"]);
                            account.Features = (AccountFeatures)Convert.ToInt32(reader["Features"]);

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
                            string type = reader["AccountType"].ToString();
                            int custId = Convert.ToInt32(reader["CustomerId"]);
                            decimal bal = Convert.ToDecimal(reader["Balance"]);
                            Account account;

                            if (type.Equals("Savings", StringComparison.OrdinalIgnoreCase))
                                account = new SavingsAccount(custId, bal);
                            else if (type.Equals("Checking", StringComparison.OrdinalIgnoreCase))
                                account = new CheckingAccount(custId, bal);
                            else
                                account = new Account(custId, type, bal);

                            account.AccountNumber = Convert.ToInt32(reader["AccountNumber"]);
                            account.Features = (AccountFeatures)Convert.ToInt32(reader["Features"]);

                            accounts.Add(account);
                        }
                    }
                }
            }

            return accounts;
        }

        // Generate next account number
        public int GetNextId()
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