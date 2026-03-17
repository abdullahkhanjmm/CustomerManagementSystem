using CustomerManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerType>().HasData(
            new CustomerType { Id = 1, Name = "Regular" },
            new CustomerType { Id = 2, Name = "Premium" },
            new CustomerType { Id = 3, Name = "VIP" }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "Ahmed Khan", Description = "Frequent buyer", Address = "123 Main St", City = "Lahore", State = "PB", Zip = "54000", CustomerTypeId = 1 },
            new Customer { Id = 2, Name = "Fatima Ali", Description = null, Address = "456 Oak Rd", City = "Karachi", State = "SD", Zip = "75000", CustomerTypeId = 2 },
            new Customer { Id = 3, Name = "Usman Tariq", Description = "Wholesale", Address = "Back Street G-10 Markaz", City = "Islamabad", State = "IS", Zip = "44000", CustomerTypeId = 1 },
            new Customer { Id = 4, Name = "Muneeb Khan", Description = "VIP client", Address = "Namak Mandi", City = "Peshawar", State = "KP", Zip = "25000", CustomerTypeId = 3 },
            new Customer { Id = 5, Name = "Bilal Hassan", Description = null, Address = "IT Park", City = "Quetta", State = "BL", Zip = "87300", CustomerTypeId = 2 }
        );
    }
}
