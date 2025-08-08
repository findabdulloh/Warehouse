using Warehouse.Service.DTOs.Resources;

namespace Warehouse.Service.Services.ResourceServices;

public interface IResourceService
{
    public Task<ResourceResultDto> CreateAsync(ResourceCreationDto creationDto);
    public Task<ResourceResultDto> UpdateAsync(ResourceUpdateDto updateDto);
    public Task<bool> DeleteAsync(long id);
    public Task<ResourceResultDto> ChangeStatusAsync(long id, bool isActive);
    public Task<ResourceResultDto> GetAsync(long id);
    public Task<List<ResourceResultDto>> GetAllAsync(bool? isActive);
}