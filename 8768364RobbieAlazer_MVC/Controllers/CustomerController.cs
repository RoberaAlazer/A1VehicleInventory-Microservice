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
        var customers = await client.GetFromJsonAsync<List<CustomerDto>>("gateway/customers/");
        return View(customers ?? new List<CustomerDto>());
    }

    public async Task<IActionResult> Details(int id)
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var customers = await client.GetFromJsonAsync<List<CustomerDto>>("gateway/customers/");
        var customer = customers?.FirstOrDefault(c => c.Id == id);

        if (customer == null) return NotFound();

        return View(customer);
    }

    public IActionResult Create()
    {
        return View(new CustomerDto(0, "", "", "", ""));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerDto dto)
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var res = await client.PostAsJsonAsync("gateway/customers/", dto);

        if (!res.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", await res.Content.ReadAsStringAsync());
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var customers = await client.GetFromJsonAsync<List<CustomerDto>>("gateway/customers/");
        var customer = customers?.FirstOrDefault(c => c.Id == id);

        if (customer == null) return NotFound();

        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CustomerDto dto)
    {
        if (id != dto.Id) return BadRequest();

        var client = _httpClientFactory.CreateClient("GatewayClient");
        var request = new HttpRequestMessage(HttpMethod.Put, $"gateway/customers/{id}")
        {
            Content = JsonContent.Create(dto)
        };

        var res = await client.SendAsync(request);

        if (!res.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", await res.Content.ReadAsStringAsync());
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        var customers = await client.GetFromJsonAsync<List<CustomerDto>>("gateway/customers/");
        var customer = customers?.FirstOrDefault(c => c.Id == id);

        if (customer == null) return NotFound();

        return View(customer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = _httpClientFactory.CreateClient("GatewayClient");
        await client.DeleteAsync($"gateway/customers/{id}");
        return RedirectToAction(nameof(Index));
    }
}

public record CustomerDto(int Id, string FirstName, string LastName, string Phone, string Email);