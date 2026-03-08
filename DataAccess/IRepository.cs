using System.Collections.Generic;

namespace BankManagementSystem.DataAccess
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T Get(int id);
        List<T> GetAll();
        int GetNextId();
    }
}
