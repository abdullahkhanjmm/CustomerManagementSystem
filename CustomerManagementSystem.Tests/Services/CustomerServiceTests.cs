using AutoMapper;
using CustomerManagementSystem.DTOs;
using CustomerManagementSystem.Models;
using CustomerManagementSystem.Repositories.Interfaces;
using CustomerManagementSystem.Services;
using Moq;
using Xunit;

namespace CustomerManagementSystem.Tests.Services;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _customerRepoMock;
    private readonly Mock<ICustomerTypeRepository> _typeRepoMock;
    private readonly IMapper _mapper;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _customerRepoMock = new Mock<ICustomerRepository>();
        _typeRepoMock = new Mock<ICustomerTypeRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.CustomerType.Name));
            cfg.CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(_ => DateTime.UtcNow));
            cfg.CreateMap<CustomerType, CustomerTypeDto>();
        });
        _mapper = config.CreateMapper();

        _service = new CustomerService(_customerRepoMock.Object, _typeRepoMock.Object, _mapper);
    }

    [Fact]
    public async Task CreateAsync_ValidType_ReturnsCustomerDto()
    {
        var dto = new CreateCustomerDto { Name = "Test", CustomerTypeId = 1 };
        _typeRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new CustomerType { Id = 1, Name = "Regular" });
        _customerRepoMock.Setup(x => x.AddAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);
        _customerRepoMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
        _customerRepoMock.Setup(x => x.GetByIdWithTypeAsync(It.IsAny<int>()))
            .ReturnsAsync(new Customer { Id = 1, Name = "Test", CustomerTypeId = 1, CustomerType = new CustomerType { Name = "Regular" } });

        var result = await _service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
        Assert.Equal("Regular", result.CustomerTypeName);
        _customerRepoMock.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_InvalidType_ThrowsKeyNotFoundException()
    {
        var dto = new CreateCustomerDto { Name = "Test", CustomerTypeId = 99 };
        _typeRepoMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((CustomerType?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.CreateAsync(dto));
    }

    [Fact]
    public async Task GetAllAsync_ReturnsPagedResponse()
    {
        var parameters = new CustomerParams { PageNumber = 1, PageSize = 10 };
        var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "A", CustomerType = new CustomerType { Name = "T" } }
        };

        _customerRepoMock.Setup(x => x.GetPagedAsync(It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((customers, 1));

        var result = await _service.GetAllAsync(parameters);

        Assert.NotNull(result);
        Assert.Single(result.Items);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistentId_ReturnsNull()
    {
        _customerRepoMock.Setup(x => x.GetByIdWithTypeAsync(999)).ReturnsAsync((Customer?)null);

        var result = await _service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_NonExistentId_ReturnsFalse()
    {
        _customerRepoMock.Setup(x => x.GetByIdAsync(999)).ReturnsAsync((Customer?)null);

        var result = await _service.DeleteAsync(999);

        Assert.False(result);
    }
}
