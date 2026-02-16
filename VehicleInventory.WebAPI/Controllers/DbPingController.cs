using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleInventory.Infrastructure.Data;

namespace VehicleInventory.WebAPI.Controllers;

[ApiController]
[Route("api/dbping")]
public class DbPingController : ControllerBase
{
    private readonly VehicleInventoryDbContext _db;

    public DbPingController(VehicleInventoryDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var vehicleCount = await _db.Vehicles.CountAsync();
        return Ok(new { ok = true, vehicleCount });
    }
}
