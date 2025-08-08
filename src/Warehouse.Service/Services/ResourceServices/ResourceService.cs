using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.IRepositories;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Service.DTOs.Resources;
using Warehouse.Service.Exceptions;

namespace Warehouse.Service.Services.ResourceServices;

public class ResourceService : IResourceService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Resource> _resourceRepository;

    public ResourceService(IMapper mapper, IRepository<Resource> resourceRepository)
    {
        _mapper = mapper;
        _resourceRepository = resourceRepository;
    }


    public async Task<ResourceResultDto> CreateAsync(ResourceCreationDto creationDto)
    {
        var resource = await _resourceRepository.SelectAsync(r => r.Name == creationDto.Name);

        if (resource is not null)
            throw new WarehouseException(409, $"Ресурс \"{resource.Name}\" уже существует");

        var mappedResource = _mapper.Map<Resource>(creationDto);

        var resourceEntity = await _resourceRepository.InsertAsync(mappedResource);

        await _resourceRepository.SaveAsync();

        return _mapper.Map<ResourceResultDto>(resourceEntity);
    }

    public async Task<ResourceResultDto> ChangeStatusAsync(long id, bool isActive)
    {
        var resource = await _resourceRepository.SelectAsync(r => r.Id == id);

        if (resource is null)
            throw new WarehouseException(404, "Ресурс не найден");

        resource.Status = isActive ? EntityStatus.Active : EntityStatus.InActive;

        var updatedEntity = _resourceRepository.Update(resource);

        await _resourceRepository.SaveAsync();

        return _mapper.Map<ResourceResultDto>(updatedEntity);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var resource = await _resourceRepository.SelectAsync(r => r.Id == id);

        if (resource is null)
            throw new WarehouseException(404, "Ресурс не найден");

        var isDeleted = await _resourceRepository.DeleteAsync(r => r.Id == id);

        await _resourceRepository.SaveAsync();

        return isDeleted;
    }

    public async Task<List<ResourceResultDto>> GetAllAsync(bool? isActive)
    {
        var resourcesQuery = _resourceRepository.SelectAll();

        if (isActive is not null)
            resourcesQuery = _resourceRepository.SelectAll(r => isActive == true ? r.Status == EntityStatus.Active : r.Status == EntityStatus.InActive);

        var resources = await resourcesQuery.OrderByDescending(r => r.Id).ToListAsync();

        return _mapper.Map<List<ResourceResultDto>>(resources);
    }

    public async Task<ResourceResultDto> GetAsync(long id)
    {
        var resource = await _resourceRepository.SelectAsync(r => r.Id == id);

        if (resource is null)
            throw new WarehouseException(404, "Ресурс не найден");

        return _mapper.Map<ResourceResultDto>(resource);
    }

    public async Task<ResourceResultDto> UpdateAsync(ResourceUpdateDto updateDto)
    {
        var resource = await _resourceRepository.SelectAsync(r => r.Id == updateDto.Id);

        if (resource is null)
            throw new WarehouseException(404, "Ресурс не найден");

        var updatedResource = _mapper.Map(updateDto, resource);
        updatedResource.UpdatedAt = DateTime.UtcNow;

        var updatedEntity = _resourceRepository.Update(updatedResource);

        await _resourceRepository.SaveAsync();

        return _mapper.Map<ResourceResultDto>(updatedEntity);
    }
}