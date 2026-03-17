using AutoMapper;
using CustomerManagementSystem.DTOs;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.CustomerType.Name));

        CreateMap<CreateCustomerDto, Customer>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateCustomerDto, Customer>()
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<CustomerType, CustomerTypeDto>();
    }
}
