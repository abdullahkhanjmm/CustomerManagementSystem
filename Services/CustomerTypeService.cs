using AutoMapper;
using CustomerManagementSystem.DTOs;
using CustomerManagementSystem.Repositories.Interfaces;
using CustomerManagementSystem.Services.Interfaces;

namespace CustomerManagementSystem.Services;

public class CustomerTypeService : ICustomerTypeService
{
    private readonly ICustomerTypeRepository _customerTypeRepository;
    private readonly IMapper _mapper;

    public CustomerTypeService(ICustomerTypeRepository customerTypeRepository, IMapper mapper)
    {
        _customerTypeRepository = customerTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerTypeDto>> GetAllAsync()
    {
        var types = await _customerTypeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerTypeDto>>(types);
    }

    public async Task<CustomerTypeDto?> GetByIdAsync(int id)
    {
        var type = await _customerTypeRepository.GetByIdAsync(id);
        return type is null ? null : _mapper.Map<CustomerTypeDto>(type);
    }
}
