using AutoMapper;
using CustomerManagementSystem.DTOs;
using CustomerManagementSystem.Models;
using CustomerManagementSystem.Repositories.Interfaces;
using CustomerManagementSystem.Services.Interfaces;

namespace CustomerManagementSystem.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerTypeRepository _customerTypeRepository;
    private readonly IMapper _mapper;

    public CustomerService(
        ICustomerRepository customerRepository, 
        ICustomerTypeRepository customerTypeRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _customerTypeRepository = customerTypeRepository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<CustomerDto>> GetAllAsync(CustomerParams customerParams)
    {
        var (items, totalCount) = await _customerRepository.GetPagedAsync(
            customerParams.SearchTerm,
            customerParams.SortBy,
            customerParams.SortDescending,
            customerParams.PageNumber,
            customerParams.PageSize);

        var dtos = _mapper.Map<IEnumerable<CustomerDto>>(items);
        return new PagedResponse<CustomerDto>(dtos, totalCount, customerParams.PageNumber, customerParams.PageSize);
    }

    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdWithTypeAsync(id);
        return customer is null ? null : _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
    {
        if (await _customerTypeRepository.GetByIdAsync(dto.CustomerTypeId) == null)
        {
            throw new KeyNotFoundException($"CustomerType with ID {dto.CustomerTypeId} not found.");
        }

        var customer = _mapper.Map<Customer>(dto);

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();

        var created = await _customerRepository.GetByIdWithTypeAsync(customer.Id);
        return _mapper.Map<CustomerDto>(created!);
    }

    public async Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerDto dto)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer is null) return null;

        if (await _customerTypeRepository.GetByIdAsync(dto.CustomerTypeId) == null)
        {
            throw new KeyNotFoundException($"CustomerType with ID {dto.CustomerTypeId} not found.");
        }

        _mapper.Map(dto, customer);

        _customerRepository.Update(customer);
        await _customerRepository.SaveChangesAsync();

        var updated = await _customerRepository.GetByIdWithTypeAsync(id);
        return _mapper.Map<CustomerDto>(updated!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer is null) return false;

        _customerRepository.Delete(customer);
        await _customerRepository.SaveChangesAsync();
        return true;
    }
}
