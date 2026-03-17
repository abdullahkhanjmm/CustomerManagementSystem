using CustomerManagementSystem.DTOs;
using CustomerManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerTypesController : ControllerBase
{
    private readonly ICustomerTypeService _customerTypeService;

    public CustomerTypesController(ICustomerTypeService customerTypeService)
    {
        _customerTypeService = customerTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerTypeDto>>> GetAll()
    {
        var customerTypes = await _customerTypeService.GetAllAsync();
        return Ok(customerTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerTypeDto>> GetById(int id)
    {
        var customerType = await _customerTypeService.GetByIdAsync(id);
        if (customerType == null)
        {
            return NotFound();
        }
        return Ok(customerType);
    }
}
