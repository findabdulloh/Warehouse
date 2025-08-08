using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Entities;
using Warehouse.Service.DTOs.InputDocuments;
using Warehouse.Service.DTOs.Resources;
using Warehouse.Service.Filters;
using Warehouse.Service.Services.InputDocumentServices;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InputDocumentsController
{
    private readonly IInputDocumentService _inputDocumentService;

    public InputDocumentsController(IInputDocumentService inputDocumentService)
    {
        _inputDocumentService = inputDocumentService;
    }

    [HttpGet("{id}")]
    public async Task<InputDocumentResultDto> GetAsync(long id)
    {
        return await _inputDocumentService.GetAsync(id);
    }

    [HttpGet]
    public async Task<List<InputDocumentResultDto>> GetAllAsync([FromQuery] InputDocumentFilter filter)
    {
        return await _inputDocumentService.GetAllAsync(filter);
    }

    [HttpPost]
    public async Task<InputDocumentResultDto> PostAsync(InputDocumentCreationDto dto)
    {
        return await _inputDocumentService.CreateAsync(dto);
    }

    [HttpPut]
    public async Task<InputDocumentResultDto> PutAsync(InputDocumentUpdateDto dto)
    {
        return await _inputDocumentService.UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(long id)
    {
        return await _inputDocumentService.DeleteAsync(id);
    }
}