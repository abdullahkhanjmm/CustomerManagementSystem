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

    public async Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedAsync(string? searchTerm, string? sortBy, bool sortDescending, int pageNumber, int pageSize)
    {
        var query = _context.Customers.Include(c => c.CustomerType).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(c => c.Name.Contains(searchTerm) || c.City.Contains(searchTerm));
        }

        query = sortBy?.ToLower() switch
        {
            "name" => sortDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
            "city" => sortDescending ? query.OrderByDescending(c => c.City) : query.OrderBy(c => c.City),
            "address" => sortDescending ? query.OrderByDescending(c => c.Address) : query.OrderBy(c => c.Address),
            _ => query.OrderBy(c => c.Id)
        };

        var totalCount = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return (items, totalCount);
    }
}
