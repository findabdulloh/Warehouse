using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Enums;
using Warehouse.Service.DTOs.Resources;
using Warehouse.Service.Services.ResourceServices;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourcesController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet("{id}")]
    public async Task<ResourceResultDto> GetAsync(long id)
    {
        return await _resourceService.GetAsync(id);
    }

    [HttpGet]
    public async Task<List<ResourceResultDto>> GetAllAsync(bool? isActive)
    {
        return await _resourceService.GetAllAsync(isActive);
    }

    [HttpPost]
    public async Task<ResourceResultDto> PostAsync(ResourceCreationDto dto)
    {
        return await _resourceService.CreateAsync(dto);
    }

    [HttpPut]
    public async Task<ResourceResultDto> PutAsync(ResourceUpdateDto dto)
    {
        return await _resourceService.UpdateAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task<ResourceResultDto> PutAsync(long id, bool isActive)
    {
        return await _resourceService.ChangeStatusAsync(id, isActive);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(long id)
    {
        return await _resourceService.DeleteAsync(id);
    }
}