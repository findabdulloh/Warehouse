using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ResourcesIndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ResourcesIndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<ResourceDto> Resources { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public bool IsActive { get; set; } = true;

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("ApiClient");

        var endpoint = IsActive
            ? "/Resources?isActive=true"
            : "/Resources?isActive=false";

        Resources = await client.GetFromJsonAsync<List<ResourceDto>>(endpoint) ?? new();
    }
}

public class ResourceDto
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public int Status { get; set; }
}
