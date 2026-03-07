using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.DataAccess;
using BankManagementSystem.Domain;

namespace BankManagementSystem.Business
{
    class TransactionService
    {
        private TransactionRepository transactionRepo;
        private AccountService accountService;

        public TransactionService()
        {
            transactionRepo = new TransactionRepository();
            accountService = new AccountService();
        }

        // Transfer money between two accounts
        public bool Transfer(int sourceAccNo, int destAccNo, decimal amount)
        {
            if (amount <= 0 || sourceAccNo == destAccNo)
                return false;

            // Get source and destination accounts
            Account sourceAcc = accountService.GetAccountByNumber(sourceAccNo);
            Account destAcc = accountService.GetAccountByNumber(destAccNo);

            if (sourceAcc == null || destAcc == null)
                return false;

            // Withdraw from source account
            bool withdrawal = sourceAcc.Withdraw(amount);
            if (!withdrawal)
                return false;

            // Deposit to destination account
            destAcc.Deposit(amount);

            // Update accounts in DB
            accountService.UpdateAccount(sourceAcc);
            accountService.UpdateAccount(destAcc);

            // Create transaction records
            Transaction t1 = new Transaction(sourceAccNo, "Transfer Debit", amount, sourceAcc.Balance);
            Transaction t2 = new Transaction(destAccNo, "Transfer Credit", amount, destAcc.Balance);

            transactionRepo.Add(t1);
            transactionRepo.Add(t2);

            return true;
        }

        // Get transaction history for an account
        public List<Transaction> GetTransactionHistory(int accountNumber)
        {
            return transactionRepo.GetByAccount(accountNumber);
        }

        // Deposit money (record transaction)
        public bool Deposit(int accountNumber, decimal amount)
        {
            bool success = accountService.Deposit(accountNumber, amount);
            if (!success)
                return false;

            Account acc = accountService.GetAccountByNumber(accountNumber);
            Transaction t = new Transaction(accountNumber, "Deposit", amount, acc.Balance);
            transactionRepo.Add(t);

            return true;
        }

        // Withdraw money (record transaction)
        public bool Withdraw(int accountNumber, decimal amount)
        {
            bool success = accountService.Withdraw(accountNumber, amount);
            if (!success)
                return false;

            Account acc = accountService.GetAccountByNumber(accountNumber);
            Transaction t = new Transaction(accountNumber, "Withdraw", amount, acc.Balance);
            transactionRepo.Add(t);

            return true;
        }
    }
}