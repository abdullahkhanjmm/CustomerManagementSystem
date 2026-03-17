using CustomerManagementSystem.Repositories;
using CustomerManagementSystem.Repositories.Interfaces;

namespace CustomerManagementSystem.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();

        return services;
    }
}
