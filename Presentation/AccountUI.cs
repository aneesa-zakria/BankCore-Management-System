using System;
using System;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.Business;
using BankManagementSystem.Domain;

namespace BankManagementSystem.Presentation
{
    class AccountUI
    {
        private AccountService accountService;

        public AccountUI()
        {
            accountService = new AccountService();
        }

        // Create new account
        public void CreateAccount()
        {
            Console.Clear();
            Console.WriteLine("===== CREATE NEW ACCOUNT =====");

            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Enter Account Type (Saving/Current): ");
            string accountType = Console.ReadLine();

            Console.Write("Enter Initial Deposit Amount: ");
            decimal balance = decimal.Parse(Console.ReadLine());

            bool success = accountService.CreateAccount(customerId, accountType, balance);

            if (success)
                Console.WriteLine("Account created successfully!");
            else
                Console.WriteLine("Failed to create account.");
        }

        // Deposit money into account
        public void Deposit()
        {
            Console.Clear();
            Console.WriteLine("===== DEPOSIT MONEY =====");

            Console.Write("Enter Account Number: ");
            int accNo = int.Parse(Console.ReadLine());

            Console.Write("Enter Deposit Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool success = accountService.Deposit(accNo, amount);

            if (success)
                Console.WriteLine("Deposit successful!");
            else
                Console.WriteLine("Deposit failed. Check account number.");
        }

        // Withdraw money from account
        public void Withdraw()
        {
            Console.Clear();
            Console.WriteLine("===== WITHDRAW MONEY =====");

            Console.Write("Enter Account Number: ");
            int accNo = int.Parse(Console.ReadLine());

            Console.Write("Enter Withdraw Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool success = accountService.Withdraw(accNo, amount);

            if (success)
                Console.WriteLine("Withdrawal successful!");
            else
                Console.WriteLine("Insufficient balance or invalid account.");
        }

        // Check account balance
        public void CheckBalance()
        {
            Console.Clear();
            Console.WriteLine("===== CHECK BALANCE =====");

            Console.Write("Enter Account Number: ");
            int accNo = int.Parse(Console.ReadLine());

            decimal? balance = accountService.GetBalance(accNo);

            if (balance != null)
                Console.WriteLine($"Account Balance: {balance:C}");
            else
                Console.WriteLine("Account not found.");
        }
    }
}