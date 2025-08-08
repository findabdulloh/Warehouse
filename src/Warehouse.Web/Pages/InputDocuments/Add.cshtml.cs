using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Warehouse.Service.DTOs.InputDocuments;
using Warehouse.Service.DTOs.InputResources;
using Warehouse.Web.Pages.MeasurementUnits;

public class CreateModel : PageModel
{
    [BindProperty]
    public InputDocumentCreationDto InputDocument { get; set; } = new InputDocumentCreationDto
    {
        Date = DateTime.UtcNow,
        InputResources = new List<InputResourceCreationDto> { new() }
    };

    public List<SelectListItem> ResourceOptions { get; set; } = new();
    public List<SelectListItem> MeasurementUnitOptions { get; set; } = new();

    private readonly HttpClient _httpClient;

    public CreateModel(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ApiClient");
    }

    public async Task OnGetAsync()
    {
        await LoadSelectLists();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadSelectLists();

        if (!ModelState.IsValid)
            return Page();

        // Ensure date is in UTC
        InputDocument.Date = DateTime.SpecifyKind(InputDocument.Date, DateTimeKind.Utc);

        var response = await _httpClient.PostAsJsonAsync("InputDocuments", InputDocument);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Ошибка при сохранении данных.");
            return Page();
        }

        return RedirectToPage("/InputDocuments/Index");
    }

    public async Task<IActionResult> OnGetAddResourceAsync(int index)
    {
        await LoadSelectLists();
        var rowModel = (index, new InputResourceCreationDto(), ResourceOptions, MeasurementUnitOptions);
        return Partial("_InputResourceRow", rowModel);
    }

    private async Task LoadSelectLists()
    {
        var resources = await _httpClient.GetFromJsonAsync<List<ResourceDto>>("Resources?isActive=true");
        var units = await _httpClient.GetFromJsonAsync<List<MeasurementUnitDto>>("MeasurementUnits?isActive=true");

        ResourceOptions = resources.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
        MeasurementUnitOptions = units.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }).ToList();
    }
}
