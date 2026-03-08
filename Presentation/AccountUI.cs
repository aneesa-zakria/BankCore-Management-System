using System;
using System;
using System.Collections.Generic;
using System.Text;
using BankManagementSystem.Business;
using BankManagementSystem.Domain;

namespace BankManagementSystem.Presentation
{
    public class AccountUI
    {
        private readonly IAccountService accountService;

        public AccountUI(IAccountService service)
        {
            accountService = service;
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

        // Manage Account Features
        public void ManageFeatures()
        {
            Console.Clear();
            Console.WriteLine("===== MANAGE ACCOUNT FEATURES =====");

            Console.Write("Enter Account Number: ");
            int accNo = int.Parse(Console.ReadLine());

            // We must retrieve the account again via concrete type to display its Features.
            if (accountService is AccountService concreteService)
            {
                Account acc = concreteService.GetAccountByNumber(accNo);
                if (acc == null)
                {
                    Console.WriteLine("Account not found.");
                    return;
                }

                Console.WriteLine($"Current Features: {acc.Features}");
            }
            else
            {
                Console.WriteLine("Account features not accessible through this service layer.");
            }
            Console.WriteLine("1. Add Online Banking");
            Console.WriteLine("2. Remove Online Banking");
            Console.WriteLine("3. Add Overdraft Protection");
            Console.WriteLine("4. Remove Overdraft Protection");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            bool success = false;
            switch (choice)
            {
                case "1":
                    success = accountService.AddFeature(accNo, AccountFeatures.OnlineBanking);
                    break;
                case "2":
                    success = accountService.RemoveFeature(accNo, AccountFeatures.OnlineBanking);
                    break;
                case "3":
                    success = accountService.AddFeature(accNo, AccountFeatures.OverdraftProtection);
                    break;
                case "4":
                    success = accountService.RemoveFeature(accNo, AccountFeatures.OverdraftProtection);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            if (success)
                Console.WriteLine("Feature updated successfully.");
            else
                Console.WriteLine("Failed to update feature.");
        }
    }
}