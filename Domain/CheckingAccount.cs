using System;

namespace BankManagementSystem.Domain
{
    public class CheckingAccount : Account
    {
        private const decimal OverdraftLimit = 500m;

        public CheckingAccount(int customerId, decimal initialBalance)
            : base(customerId, "Checking", initialBalance)
        {
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be positive");

            // Allow overdraft up to limit for Checking Account
            if (Balance - amount < -OverdraftLimit)
                return false;

            Balance -= amount;
            return true;
        }
    }
}
