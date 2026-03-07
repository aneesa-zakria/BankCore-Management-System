using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BankManagementSystem.Domain;

namespace BankManagementSystem.DataAccess
{
    public class CustomerRepository
    {
        // Add customer
        public void Add(Customer customer)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Customers (CustomerId, Name, Phone, Email) VALUES (@Id, @Name, @Phone, @Email)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update customer
        public void Update(Customer customer)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "UPDATE Customers SET Name=@Name, Phone=@Phone, Email=@Email WHERE CustomerId=@Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete customer
        public void Delete(int customerId)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Customers WHERE CustomerId=@Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", customerId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get customer by ID
        public Customer Get(int customerId)
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Customers WHERE CustomerId=@Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Customer customer = new Customer(
                                reader["Name"].ToString(),
                                reader["Phone"].ToString(),
                                reader["Email"].ToString()
                            );

                            customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);

                            return customer;
                        }
                    }
                }
            }

            return null;
        }

        // Get all customers
        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Customers";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer(
                                reader["Name"].ToString(),
                                reader["Phone"].ToString(),
                                reader["Email"].ToString()
                            );

                            customer.CustomerId = Convert.ToInt32(reader["CustomerId"]);

                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }

        // Generate next customer ID
        public int GetNextCustomerId()
        {
            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();

                string query = "SELECT ISNULL(MAX(CustomerId),0) + 1 FROM Customers";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}