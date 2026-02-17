using Microsoft.AspNetCore.Mvc;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Services;
using VehicleInventory.Domain.Exceptions;
namespace VehicleInventory.WebAPI.Controllers;
[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly VehicleService _service;
    public VehiclesController(VehicleService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<VehicleResponse>>> GetAll()
        => Ok(await _service.GetAllVehiclesAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleResponse>> GetById(int id)
    {
        var v = await _service.GetVehicleByIdAsync(id);
        if (v == null) return NotFound();
        return Ok(v);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateVehicleRequest req)
    {
        try
        {
            var id = await _service.CreateVehicleAsync(req);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateVehicleStatusRequest req)
    {
        try
        {
            await _service.UpdateVehicleStatusAsync(id, req.StatusId);
            return NoContent();
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteVehicleAsync(id);
            return NoContent();
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
