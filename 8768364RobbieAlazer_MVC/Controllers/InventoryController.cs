using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace _8768364RobbieAlazer_MVC.Controllers;

public class InventoryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InventoryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("InventoryApi");
        var vehicles = await client.GetFromJsonAsync<List<VehicleDto>>("api/vehicles");
        return View(vehicles ?? new List<VehicleDto>());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicleDto dto)
    {
        var client = _httpClientFactory.CreateClient("InventoryApi");
        var res = await client.PostAsJsonAsync("api/vehicles", dto);
        if (!res.IsSuccessStatusCode) TempData["Err"] = await res.Content.ReadAsStringAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, int statusId)
    {
        var client = _httpClientFactory.CreateClient("InventoryApi");
        var res = await client.PutAsJsonAsync($"api/vehicles/{id}/status", new { statusId });
        if (!res.IsSuccessStatusCode) TempData["Err"] = await res.Content.ReadAsStringAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient("InventoryApi");
        var res = await client.DeleteAsync($"api/vehicles/{id}");
        if (!res.IsSuccessStatusCode) TempData["Err"] = await res.Content.ReadAsStringAsync();
        return RedirectToAction(nameof(Index));
    }
}
public record VehicleDto(int Id, string VehicleCode, int LocationId, string VehicleType, int Status);
public record CreateVehicleDto(string VehicleCode, int LocationId, string VehicleType);