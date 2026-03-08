using System;
using BankManagementSystem.Business;

namespace BankManagementSystem.Presentation
{
    public class CustomerUI
    {
        private readonly ICustomerService customerService;

        public CustomerUI(ICustomerService service)
        {
            customerService = service;
        }

        public void CreateCustomer()
        {
            Console.Clear();
            Console.WriteLine("===== CREATE NEW CUSTOMER =====");

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            bool success = customerService.CreateCustomer(name, phone, email);

            if (success)
                Console.WriteLine("Customer created successfully!");
            else
                Console.WriteLine("Failed to create customer.");
        }
    }
}
