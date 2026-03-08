namespace BankManagementSystem.Business
{
    public interface ICustomerService
    {
        bool CreateCustomer(string name, string phone, string email);
    }
}
