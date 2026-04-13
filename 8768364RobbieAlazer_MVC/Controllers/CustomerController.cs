using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace _8768364RobbieAlazer_MVC.Controllers;

public class CustomersController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomersController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var customers = await client.GetFromJsonAsync<List<CustomerDto>>("gateway/customers");
        return View(customers ?? new List<CustomerDto>());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CustomerDto dto)
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var res = await client.PostAsJsonAsync("gateway/customers", dto);
        if (!res.IsSuccessStatusCode) TempData["Err"] = await res.Content.ReadAsStringAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var res = await client.DeleteAsync($"gateway/customers/{id}");
        if (!res.IsSuccessStatusCode) TempData["Err"] = await res.Content.ReadAsStringAsync();
        return RedirectToAction(nameof(Index));
    }
}

public record CustomerDto(int Id, string FirstName, string LastName, string Phone, string Email);