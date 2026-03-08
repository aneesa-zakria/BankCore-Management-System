using System;
using System;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.Business;
using BankManagementSystem.Domain;

namespace BankManagementSystem.Presentation
{
    public class TransactionUI
    {
        private readonly ITransactionService transactionService;

        public TransactionUI(ITransactionService service)
        {
            transactionService = service;
        }

        // Transfer money from one account to another
        public void TransferMoney()
        {
            Console.Clear();
            Console.WriteLine("===== TRANSFER MONEY =====");

            Console.Write("Enter Source Account Number: ");
            int sourceAcc = int.Parse(Console.ReadLine());

            Console.Write("Enter Destination Account Number: ");
            int destAcc = int.Parse(Console.ReadLine());

            Console.Write("Enter Amount to Transfer: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool success = transactionService.Transfer(sourceAcc, destAcc, amount);

            if (success)
                Console.WriteLine("Transfer completed successfully!");
            else
                Console.WriteLine("Transfer failed. Check account numbers or balance.");
        }

        // View transaction history for an account
        public void ViewTransactionHistory()
        {
            Console.Clear();
            Console.WriteLine("===== TRANSACTION HISTORY =====");

            Console.Write("Enter Account Number: ");
            int accNo = int.Parse(Console.ReadLine());

            var transactions = transactionService.GetTransactionHistory(accNo);

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            Console.WriteLine("Date\t\tType\tAmount\tBalanceAfter");
            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Date}\t{t.Type}\t{t.Amount:C}\t{t.BalanceAfter:C}");
            }
        }
    }
}