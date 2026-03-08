namespace BankManagementSystem.Business
{
    public interface IAccountService
    {
        bool CreateAccount(int customerId, string accountType, decimal initialBalance);
        bool Deposit(int accountNumber, decimal amount);
        bool Withdraw(int accountNumber, decimal amount);
        decimal? GetBalance(int accountNumber);
        bool AddFeature(int accountNumber, Domain.AccountFeatures feature);
        bool RemoveFeature(int accountNumber, Domain.AccountFeatures feature);
        Domain.Account GetAccountByNumber(int accountNumber);
    }
}
