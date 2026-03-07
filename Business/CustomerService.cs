
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.DataAccess;
using BankManagementSystem.Domain;

namespace BankManagementSystem.Business
{
    class CustomerService
    {
        private CustomerRepository customerRepo;

        public CustomerService()
        {
            customerRepo = new CustomerRepository();
        }

        // Create a new customer
        public bool CreateCustomer(string name, string phone, string email)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrWhiteSpace(name))
                    return false;
                if (string.IsNullOrWhiteSpace(phone))
                    return false;
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                // Generate unique CustomerID
                int newCustomerId = GenerateCustomerId();

                // Create Customer object (Encapsulation)
                Customer newCustomer = GetNewCustomer(name, phone, email, newCustomerId);

                // Save to repository (database)
                customerRepo.Add(newCustomer);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Customer GetNewCustomer(string name, string phone, string email, int newCustomerId)
        {
            return new Customer(name, phone, email)
            {
                CustomerId = newCustomerId
            };
        }

        // Update existing customer
        public bool UpdateCustomer(int customerId, string name, string phone, string email)
        {
            Customer customer = customerRepo.Get(customerId);
            if (customer == null)
                return false;

            // Update fields (encapsulation via properties)
            customer.Name = name;
            customer.Phone = phone;
            customer.Email = email;

            customerRepo.Update(customer);
            return true;
        }

        // Get customer by ID
        public Customer GetCustomer(int customerId)
        {
            return customerRepo.Get(customerId);
        }

        // Get all customers
        public List<Customer> GetAllCustomers()
        {
            return customerRepo.GetAll();
        }

        // Delete customer
        public bool DeleteCustomer(int customerId)
        {
            Customer customer = customerRepo.Get(customerId);
            if (customer == null)
                return false;

            customerRepo.Delete(customerId);
            return true;
        }

        // Generate unique Customer ID
        private int GenerateCustomerId()
        {
            return customerRepo.GetNextCustomerId();
        }
    }
}