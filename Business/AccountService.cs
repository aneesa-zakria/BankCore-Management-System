using System;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.DataAccess;
using BankManagementSystem.Domain;


namespace BankManagementSystem.Business
{
    public class AccountService : IAccountService
    {
        private IRepository<Account> accountRepo;

        public AccountService(IRepository<Account> repository)
        {
            accountRepo = repository;
        }

        // Create a new account
        public bool CreateAccount(int customerId, string accountType, decimal initialBalance)
        {
            try
            {
                // Validate inputs
                if (initialBalance < 0)
                    return false;
                if (string.IsNullOrWhiteSpace(accountType))
                    return false;

                // Generate unique account number
                int newAccNo = GenerateAccountNumber();

                // Create a new Account object using polymorphism
                Account newAccount;
                if (accountType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
                    newAccount = new SavingsAccount(customerId, initialBalance);
                else if (accountType.Equals("Checking", StringComparison.OrdinalIgnoreCase))
                    newAccount = new CheckingAccount(customerId, initialBalance);
                else
                    newAccount = new Account(customerId, accountType, initialBalance);
                newAccount.SetAccountNumber(newAccNo);

                // Save account in the repository (database)
                accountRepo.Add(newAccount);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Deposit money into account
        public bool Deposit(int accountNumber, decimal amount)
        {
            if (amount <= 0)
                return false;

            Account acc = accountRepo.Get(accountNumber);
            if (acc == null)
                return false;

            try
            {
                acc.Deposit(amount); // encapsulated method
                accountRepo.Update(acc); // persist changes
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Withdraw money from account
        public bool Withdraw(int accountNumber, decimal amount)
        {
            if (amount <= 0)
                return false;

            Account acc = accountRepo.Get(accountNumber);
            if (acc == null)
                return false;

            bool success = acc.Withdraw(amount); // encapsulated method
            if (success)
            {
                accountRepo.Update(acc); // persist changes
                return true;
            }
            return false;
        }

        // Get account balance
        public decimal? GetBalance(int accountNumber)
        {
            Account acc = accountRepo.Get(accountNumber);
            if (acc == null)
                return null;
            return acc.Balance; // encapsulation ensures safe read
        }

        // Add a feature to an account
        public bool AddFeature(int accountNumber, AccountFeatures feature)
        {
            Account acc = accountRepo.Get(accountNumber);
            if (acc == null) return false;

            acc.AddFeature(feature);
            accountRepo.Update(acc);
            return true;
        }

        // Remove a feature from an account
        public bool RemoveFeature(int accountNumber, AccountFeatures feature)
        {
            Account acc = accountRepo.Get(accountNumber);
            if (acc == null) return false;

            acc.RemoveFeature(feature);
            accountRepo.Update(acc);
            return true;
        }

        // Generate next account number
        private int GenerateAccountNumber()
        {
            return accountRepo.GetNextId();
        }

        // Get account by number (for internal use by other services)
        public Account GetAccountByNumber(int accountNumber)
        {
            return accountRepo.Get(accountNumber);
        }

        // Update account state
        internal void UpdateAccount(Account account)
        {
            accountRepo.Update(account);
        }
    }
}