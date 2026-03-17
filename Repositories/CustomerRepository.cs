using CustomerManagementSystem.Data;
using CustomerManagementSystem.Models;
using CustomerManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Customer>> GetAllWithTypeAsync()
    {
        return await _context.Customers
            .Include(c => c.CustomerType)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdWithTypeAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.CustomerType)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
