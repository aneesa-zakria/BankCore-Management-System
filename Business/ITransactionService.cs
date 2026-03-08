using System.Collections.Generic;
using BankManagementSystem.Domain;

namespace BankManagementSystem.Business
{
    public interface ITransactionService
    {
        bool Transfer(int fromAccount, int toAccount, decimal amount);
        List<Transaction> GetTransactionHistory(int accountNumber);
    }
}
