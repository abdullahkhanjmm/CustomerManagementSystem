using CustomerManagementSystem.DTOs;
using CustomerManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<CustomerDto>>> GetAll([FromQuery] CustomerParams customerParams)
    {
        var result = await _customerService.GetAllAsync(customerParams);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create(CreateCustomerDto createCustomerDto)
    {
        var customer = await _customerService.CreateAsync(createCustomerDto);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCustomerDto updateCustomerDto)
    {
        var updatedCustomer = await _customerService.UpdateAsync(id, updateCustomerDto);
        if (updatedCustomer == null)
        {
            return NotFound();
        }
        return Ok(updatedCustomer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _customerService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
