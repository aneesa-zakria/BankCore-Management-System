using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain
{
    public class Account
    {
        // Private fields (encapsulation)
        private int accountNumber;
        private int customerId;
        private string accountType;
        private decimal balance;

        // Public properties (read/write controlled)
        public int AccountNumber
        {
            get { return accountNumber; }
            internal set { accountNumber = value; } // only class can set
        }

        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        public decimal Balance
        {
            get { return balance; } // read-only from outside
        }

        // Constructor
        public Account(int customerId, string accountType, decimal initialBalance)
        {
            this.customerId = customerId;
            this.accountType = accountType;
            if (initialBalance < 0)
                throw new ArgumentException("Balance cannot be negative");
            this.balance = initialBalance;
        }

        // Deposit method (encapsulated)
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive");
            balance += amount;
        }

        // Withdraw method (encapsulated)
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be positive");

            if (amount > balance)
                return false;

            balance -= amount;
            return true;
        }

        // Internal method to set AccountNumber (used by AccountService)
        internal void SetAccountNumber(int accNo)
        {
            accountNumber = accNo;
        }
    }
}