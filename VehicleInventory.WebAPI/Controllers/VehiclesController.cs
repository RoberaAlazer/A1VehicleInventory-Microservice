using Microsoft.AspNetCore.Mvc;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Services;

namespace VehicleInventory.WebAPI.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly VehicleService _service;

    public VehiclesController(VehicleService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<RAVehicleResponse>>> GetAll()
        => Ok(await _service.GetAllVehiclesAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RAVehicleResponse>> GetById(int id)
    {
        var v = await _service.GetVehicleByIdAsync(id);
        if (v == null) return NotFound();
        return Ok(v);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] RACreateVehicleRequest req)
    {
        var id = await _service.CreateVehicleAsync(req);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] RAUpdateVehicleStatusRequest req)
    {
        await _service.UpdateVehicleStatusAsync(id, req.StatusId);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteVehicleAsync(id);
        return NoContent();
    }
}