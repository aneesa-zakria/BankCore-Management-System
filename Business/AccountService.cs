using System;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.DataAccess;
using BankManagementSystem.Domain;


namespace BankManagementSystem.Business
{
    class AccountService
    {
        private AccountRepository accountRepo;

        public AccountService()
        {
            accountRepo = new AccountRepository();
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

                // Create a new Account object (encapsulation used)
                Account newAccount = new Account(customerId, accountType, initialBalance);
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

        // Generate unique account number
        private int GenerateAccountNumber()
        {
            return accountRepo.GetNextAccountNumber();
        }

        // Get account by number (for internal use by other services)
        internal Account GetAccountByNumber(int accountNumber)
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