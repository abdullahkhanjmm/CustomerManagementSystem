using AutoMapper;
using CustomerManagementSystem.DTOs;
using CustomerManagementSystem.Models;
using CustomerManagementSystem.Repositories.Interfaces;
using CustomerManagementSystem.Services.Interfaces;

namespace CustomerManagementSystem.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllWithTypeAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdWithTypeAsync(id);
        return customer is null ? null : _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
    {
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
