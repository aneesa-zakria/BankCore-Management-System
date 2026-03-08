using System;
using BankManagementSystem.Domain;
using BankManagementSystem.Presentation;

namespace BankManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            DbMigrator.EnsureDatabaseSchema();

            // Initialize Repositories (Data Access Layer)
            DataAccess.IRepository<Account> accountRepo = new DataAccess.AccountRepository();
            DataAccess.IRepository<Customer> customerRepo = new DataAccess.CustomerRepository();
            DataAccess.IRepository<Transaction> transactionRepo = new DataAccess.TransactionRepository();

            // Initialize Services (Business Layer)
            Business.IAccountService accountService = new Business.AccountService(accountRepo);
            Business.ICustomerService customerService = new Business.CustomerService(customerRepo);
            // TransactionService needs both its repo and the account service
            Business.ITransactionService transactionService = new Business.TransactionService(transactionRepo, accountService);

            // Initialize UI (Presentation Layer)
            MainMenu menu = new MainMenu();
            AccountUI accountUI = new AccountUI(accountService);
            CustomerUI customerUI = new CustomerUI(customerService);
            TransactionUI transactionUI = new TransactionUI(transactionService);

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
                        accountUI.ManageFeatures();
                        break;

                    case 9:
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