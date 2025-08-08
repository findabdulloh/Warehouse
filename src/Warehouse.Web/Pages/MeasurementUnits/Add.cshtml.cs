using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Web.Pages.MeasurementUnits;

public class AddMeasurementUnitModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AddMeasurementUnitModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    [Required(ErrorMessage = "Наименование обязательно")]
    public string Name { get; set; } = "";

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var client = _httpClientFactory.CreateClient("ApiClient");

        var newMeasurementUnit = new { Name };

        var response = await client.PostAsJsonAsync("/MeasurementUnits", newMeasurementUnit);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("Index", new { isArchive = false });
        }

        ModelState.AddModelError(string.Empty, "Ошибка при создании ресурса");
        return Page();
    }
}
