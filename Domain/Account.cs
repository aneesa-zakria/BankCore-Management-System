using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain
{
    [Flags]
    public enum AccountFeatures
    {
        None = 0,
        OnlineBanking = 1 << 0, // 1
        PaperStatements = 1 << 1, // 2
        OverdraftProtection = 1 << 2, // 4
        InternationalTransfers = 1 << 3 // 8
    }

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
            protected set { balance = value; } // settable by derived classes
        }

        // Bitwise Features property
        public AccountFeatures Features { get; internal set; } = AccountFeatures.None;

        // Constructor
        public Account(int customerId, string accountType, decimal initialBalance)
        {
            this.customerId = customerId;
            this.accountType = accountType;
            this.balance = initialBalance;
        }

        // Deposit method (encapsulated)
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive");
            balance += amount;
        }

        // Withdraw method (encapsulated, virtual for polymorphism)
        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be positive");

            if (amount > balance)
                return false;

            balance -= amount;
            return true;
        }

        // Bitwise operator methods
        public void AddFeature(AccountFeatures feature)
        {
            Features |= feature; // Bitwise OR
        }

        public void RemoveFeature(AccountFeatures feature)
        {
            Features &= ~feature; // Bitwise AND and NOT
        }

        public bool HasFeature(AccountFeatures feature)
        {
            return (Features & feature) == feature; // Bitwise AND
        }

        // Internal method to set AccountNumber (used by AccountService)
        internal void SetAccountNumber(int accNo)
        {
            accountNumber = accNo;
        }
    }
}