using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Warehouse.Web.Pages.MeasurementUnits;

public class MeasurementUnitsIndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MeasurementUnitsIndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<MeasurementUnitDto> Resources { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public bool IsActive { get; set; } = true;

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("ApiClient");

        var endpoint = IsActive
            ? "/MeasurementUnits?isActive=true"
            : "/MeasurementUnits?isActive=false";

        Resources = await client.GetFromJsonAsync<List<MeasurementUnitDto>>(endpoint) ?? new();
    }
}

public class MeasurementUnitDto
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public int Status { get; set; }
}
