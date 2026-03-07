using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.Domain;
using Microsoft.Data.SqlClient;

namespace BankManagementSystem.DataAccess
{
    public class TransactionRepository
    {
        // Add transaction
        public void Add(Transaction transaction)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Transactions 
                                (AccountNumber, Type, Amount, BalanceAfter, Date) 
                                VALUES (@AccountNumber, @Type, @Amount, @BalanceAfter, @Date)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", transaction.AccountNumber);
                    cmd.Parameters.AddWithValue("@Type", transaction.Type);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                    cmd.Parameters.AddWithValue("@BalanceAfter", transaction.BalanceAfter);
                    cmd.Parameters.AddWithValue("@Date", transaction.Date);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get transactions for an account
        public List<Transaction> GetByAccount(int accountNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Transactions WHERE AccountNumber=@AccountNumber";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction transaction = new Transaction(
                                Convert.ToInt32(reader["AccountNumber"]),
                                reader["Type"].ToString(),
                                Convert.ToDecimal(reader["Amount"]),
                                Convert.ToDecimal(reader["BalanceAfter"])
                            );

                            transaction.TransactionId = Convert.ToInt32(reader["TransactionId"]);
                            transaction.Date = Convert.ToDateTime(reader["Date"]);

                            transactions.Add(transaction);
                        }
                    }
                }
            }

            return transactions;
        }

        // Get all transactions
        public List<Transaction> GetAll()
        {
            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Transactions";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction transaction = new Transaction(
                                Convert.ToInt32(reader["AccountNumber"]),
                                reader["Type"].ToString(),
                                Convert.ToDecimal(reader["Amount"]),
                                Convert.ToDecimal(reader["BalanceAfter"])
                            );

                            transaction.TransactionId = Convert.ToInt32(reader["TransactionId"]);
                            transaction.Date = Convert.ToDateTime(reader["Date"]);

                            transactions.Add(transaction);
                        }
                    }
                }
            }

            return transactions;
        }
    }
}