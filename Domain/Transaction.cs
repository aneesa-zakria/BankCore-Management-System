using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain
{
    public class Transaction
    {
        private int transactionId;
        private int accountNumber;
        private string type;
        private decimal amount;
        private decimal balanceAfter;
        private DateTime date;

        public int TransactionId
        {
            get { return transactionId; }
            internal set { transactionId = value; }
        }
        public int AccountNumber
        {
            get { return accountNumber; }
            private set { accountNumber = value; }
        }
        public string Type
        {
            get { return type; }
            private set { type = value; }
        }
        public decimal Amount
        {
            get { return amount; }
            private set { amount = value; }
        }
        public decimal BalanceAfter
        {
            get { return balanceAfter; }
            private set { balanceAfter = value; }
        }
        public DateTime Date
        {
            get { return date; }
            internal set { date = value; }
        }

        public Transaction(int accountNumber, string type, decimal amount, decimal balanceAfter)
        {
            this.accountNumber = accountNumber;
            this.type = type;
            this.amount = amount;
            this.balanceAfter = balanceAfter;
            this.date = DateTime.Now;
        }
    }
}