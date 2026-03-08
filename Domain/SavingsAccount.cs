using System;

namespace BankManagementSystem.Domain
{
    public class SavingsAccount : Account
    {
        private const decimal MinimumBalance = 100m;

        public SavingsAccount(int customerId, decimal initialBalance) 
            : base(customerId, "Savings", initialBalance)
        {
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be positive");

            // Enforce minimum balance for Savings Account
            if (Balance - amount < MinimumBalance)
                return false;

            Balance -= amount;
            return true;
        }
    }
}
