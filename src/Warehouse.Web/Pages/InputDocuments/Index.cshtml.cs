using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Service.DTOs.InputDocuments;
using Warehouse.Service.DTOs.MeasurementUnits;
using Warehouse.Service.DTOs.Resources;

public class InputDocumentsModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _clientFactory;

    public InputDocumentsModel(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("ApiClient");
    }

    [BindProperty(SupportsGet = true)]
    public DateTime? From { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? To { get; set; }

    [BindProperty(SupportsGet = true)]
    public List<long>? InputDocumentIds { get; set; }

    [BindProperty(SupportsGet = true)]
    public List<long>? ResourceIds { get; set; }

    [BindProperty(SupportsGet = true)]
    public List<long>? MeasurementUnitIds { get; set; }

    public List<InputDocumentResultDto> InputDocuments { get; set; } = new();
    public List<ResourceResultDto> Resources { get; set; } = new();
    public List<MeasurementUnitResultDto> MeasurementUnits { get; set; } = new();
    public List<InputDocumentResultDto> AllInputDocuments { get; set; } = new();

    public async Task OnGetAsync()
    {
        var queryParams = new List<string>();
        if (From.HasValue) queryParams.Add($"From={From.Value:yyyy-MM-ddZ}");
        if (To.HasValue) queryParams.Add($"To={To.Value:yyyy-MM-ddZ}");
        if (InputDocumentIds != null) queryParams.AddRange(InputDocumentIds.Select(n => $"InputDocumentIds={n}"));
        if (ResourceIds != null) queryParams.AddRange(ResourceIds.Select(r => $"ResourceIds={r}"));
        if (MeasurementUnitIds != null) queryParams.AddRange(MeasurementUnitIds.Select(m => $"MeasurementIds={m}"));

        var queryString = string.Join("&", queryParams);
        var url = string.IsNullOrEmpty(queryString) ? "InputDocuments" : $"InputDocuments?{queryString}";

        var result = await _httpClient.GetFromJsonAsync<List<InputDocumentResultDto>>(url);
        InputDocuments = result ?? new();

        Resources = (await _httpClient.GetFromJsonAsync<List<ResourceResultDto>>("Resources")) ?? new();
        MeasurementUnits = (await _httpClient.GetFromJsonAsync<List<MeasurementUnitResultDto>>("MeasurementUnits")) ?? new();
        AllInputDocuments = (await _httpClient.GetFromJsonAsync<List<InputDocumentResultDto>>("InputDocuments")) ?? new();
    }
}
