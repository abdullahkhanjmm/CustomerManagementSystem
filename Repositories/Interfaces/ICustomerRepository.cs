using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllWithTypeAsync();
    Task<Customer?> GetByIdWithTypeAsync(int id);
}
