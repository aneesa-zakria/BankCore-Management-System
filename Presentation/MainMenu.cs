using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Presentation
{
    class MainMenu
    {
        public int ShowMenu()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("      BANK MANAGEMENT SYSTEM     ");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. Create Account");
            Console.WriteLine("3. Deposit Money");
            Console.WriteLine("4. Withdraw Money");
            Console.WriteLine("5. Transfer Money");
            Console.WriteLine("6. Check Balance");
            Console.WriteLine("7. Transaction History");
            Console.WriteLine("8. Manage Account Features");
            Console.WriteLine("9. Exit");
            Console.WriteLine("=================================");

            Console.Write("Enter your choice: ");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            return choice;
        }
    }
}