using CustomerManagementSystem.Repositories;
using CustomerManagementSystem.Repositories.Interfaces;
using CustomerManagementSystem.Services;
using CustomerManagementSystem.Services.Interfaces;

namespace CustomerManagementSystem.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ICustomerTypeService, CustomerTypeService>();

        return services;
    }
}
