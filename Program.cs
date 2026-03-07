using BankManagementSystem.Domain;
using BankManagementSystem.Presentation;
using System;

namespace BankManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            AccountUI accountUI = new AccountUI();
            CustomerUI customerUI = new CustomerUI();
            TransactionUI transactionUI = new TransactionUI();

            bool running = true;

            while (running)
            {
                int choice = menu.ShowMenu();

                switch (choice)
                {
                    case 1:
                        customerUI.CreateCustomer();
                        break;

                    case 2:
                        accountUI.CreateAccount();
                        break;

                    case 3:
                        accountUI.Deposit();
                        break;

                    case 4:
                        accountUI.Withdraw();
                        break;

                    case 5:
                        transactionUI.TransferMoney();
                        break;

                    case 6:
                        accountUI.CheckBalance();
                        break;

                    case 7:
                        transactionUI.ViewTransactionHistory();
                        break;

                    case 8:
                        Console.WriteLine("Exiting system...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}