using CustomerManagementSystem.DTOs;

namespace CustomerManagementSystem.Services.Interfaces;

public interface ICustomerService
{
    Task<PagedResponse<CustomerDto>> GetAllAsync(CustomerParams customerParams);
    Task<CustomerDto?> GetByIdAsync(int id);
    Task<CustomerDto> CreateAsync(CreateCustomerDto dto);
    Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerDto dto);
    Task<bool> DeleteAsync(int id);
}
