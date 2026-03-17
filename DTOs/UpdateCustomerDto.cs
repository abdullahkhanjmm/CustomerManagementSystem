using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.DTOs;

public class UpdateCustomerDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1024)]
    public string? Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(2)]
    public string State { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Zip { get; set; } = string.Empty;

    [Required]
    public int CustomerTypeId { get; set; }
}
