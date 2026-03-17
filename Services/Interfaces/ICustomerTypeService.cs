using CustomerManagementSystem.DTOs;

namespace CustomerManagementSystem.Services.Interfaces;

public interface ICustomerTypeService
{
    Task<IEnumerable<CustomerTypeDto>> GetAllAsync();
    Task<CustomerTypeDto?> GetByIdAsync(int id);
}
