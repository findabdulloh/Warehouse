using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;
using Warehouse.Service.DTOs.InputDocuments;
using Warehouse.Web.Pages.MeasurementUnits;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _clientFactory = httpClientFactory;
    }

    [BindProperty]
    public InputDocumentUpdateDto InputDocument { get; set; }
    public InputDocumentResultDto InputDocumentResult { get; set; }

    public List<SelectListItem> ResourceOptions { get; set; } = new();
    public List<SelectListItem> MeasurementUnitOptions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _clientFactory.CreateClient("ApiClient");
        var document = await client.GetStringAsync($"/InputDocuments/{id}");
        if (document == null)
        {
            return NotFound();
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        InputDocumentResult = JsonSerializer.Deserialize<InputDocumentResultDto>(document, options);

        InputDocument = JsonSerializer.Deserialize<InputDocumentUpdateDto>(document, options);

        await LoadDropdownsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return Page();
        }

        InputDocument.Date = InputDocument.Date.ToUniversalTime();

        var json = JsonSerializer.Serialize(InputDocument);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PutAsync($"/InputDocuments", content);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при сохранении документа");
            await LoadDropdownsAsync();
            return Page();
        }

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.DeleteAsync($"/InputDocuments/{id}");

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при удалении документа");
            return Page();
        }

        return RedirectToPage("Index");
    }

    private async Task LoadDropdownsAsync()
    {
        var client = _clientFactory.CreateClient("ApiClient");
        var resources = await client.GetFromJsonAsync<List<ResourceDto>>("/Resources?isActive=true");
        var units = await client.GetFromJsonAsync<List<MeasurementUnitDto>>("/MeasurementUnits?isActive=true");

        ResourceOptions = resources.Select(r =>
            new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();

        MeasurementUnitOptions = units.Select(u =>
            new SelectListItem { Value = u.Id.ToString(), Text = u.Name }).ToList();
    }
}
