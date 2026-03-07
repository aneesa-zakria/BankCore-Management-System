using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain
{
    public class Customer
    {
        // Private fields (encapsulation)
        private int customerId;
        private string name;
        private string phone;
        private string email;

        // Composition: Customer can have multiple accounts
        private List<Account> accounts;

        // Public properties
        public int CustomerId
        {
            get { return customerId; }
            internal set { customerId = value; } // only set internally
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");
                name = value;
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone cannot be empty");
                phone = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be empty");
                email = value;
            }
        }

        public List<Account> Accounts
        {
            get { return accounts; }
        }

        // Constructor
        public Customer(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
            accounts = new List<Account>();
        }

        // Add an account to the customer
        public void AddAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account cannot be null");

            accounts.Add(account);
        }

        // Remove an account from the customer
        public bool RemoveAccount(Account account)
        {
            return accounts.Remove(account);
        }
    }
}