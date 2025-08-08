using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Warehouse.Web.Pages.MeasurementUnits;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public EditModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [BindProperty]
    public MeasurementUnitDto MeasurementUnit { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(long id)
    {
        var client = _clientFactory.CreateClient("ApiClient");
        var result = await client.GetFromJsonAsync<MeasurementUnitDto>($"MeasurementUnits/{id}");

        if (result == null)
            return NotFound();

        MeasurementUnit = result;
        return Page();
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PutAsJsonAsync($"MeasurementUnits", MeasurementUnit);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при сохранении ресурса.");
            return Page();
        }

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostArchiveAsync()
    {
        var client = _clientFactory.CreateClient("ApiClient");

        MeasurementUnit.Status = 1;
        var response = await client.PutAsync($"MeasurementUnits/{MeasurementUnit.Id}?isActive=false", null);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при архивировании ресурса.");
            return Page();
        }

        return RedirectToPage("Index", new { isArchive = true });
    }

    public async Task<IActionResult> OnPostActivateAsync()
    {
        var client = _clientFactory.CreateClient("ApiClient");

        MeasurementUnit.Status = 0;
        var response = await client.PutAsync($"MeasurementUnits/{MeasurementUnit.Id}?isActive=true", null);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при активации ресурса.");
            return Page();
        }

        return RedirectToPage("Index", new { isArchive = false });
    }


    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var client = _clientFactory.CreateClient("ApiClient");

        var response = await client.DeleteAsync($"MeasurementUnits/{MeasurementUnit.Id}");

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при удалении ресурса.");
            return Page();
        }

        return RedirectToPage("Index");
    }
}
