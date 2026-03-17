using CustomerManagementSystem.Data;
using CustomerManagementSystem.Models;
using CustomerManagementSystem.Repositories.Interfaces;

namespace CustomerManagementSystem.Repositories;

public class CustomerTypeRepository : Repository<CustomerType>, ICustomerTypeRepository
{
    public CustomerTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
