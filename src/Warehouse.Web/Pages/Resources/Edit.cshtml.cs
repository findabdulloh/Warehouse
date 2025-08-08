using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Warehouse.Web.Pages.Resources;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public EditModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [BindProperty]
    public ResourceDto Resource { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(long id)
    {
        var client = _clientFactory.CreateClient("ApiClient");
        var result = await client.GetFromJsonAsync<ResourceDto>($"Resources/{id}");

        if (result == null)
            return NotFound();

        Resource = result;
        return Page();
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PutAsJsonAsync($"Resources", Resource);

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

        Resource.Status = 1;
        var response = await client.PutAsync($"Resources/{Resource.Id}?isActive=false", null);

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

        Resource.Status = 0;
        var response = await client.PutAsync($"Resources/{Resource.Id}?isActive=true", null);

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

        var response = await client.DeleteAsync($"Resources/{Resource.Id}");

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при удалении ресурса.");
            return Page();
        }

        return RedirectToPage("Index");
    }
}
