using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class AddResourceModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AddResourceModel(IHttpClientFactory httpClientFactory)
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

        var newResource = new { Name };

        var response = await client.PostAsJsonAsync("/Resources", newResource);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("Index", new { isArchive = false });
        }

        ModelState.AddModelError(string.Empty, "Ошибка при создании ресурса");
        return Page();
    }
}
