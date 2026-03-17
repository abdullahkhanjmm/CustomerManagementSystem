using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllWithTypeAsync();
    Task<Customer?> GetByIdWithTypeAsync(int id);
    Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedAsync(string? searchTerm, string? sortBy, bool sortDescending, int pageNumber, int pageSize);
}
