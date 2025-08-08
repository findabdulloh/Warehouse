using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.IRepositories;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Service.DTOs.InputDocuments;
using Warehouse.Service.DTOs.InputResources;
using Warehouse.Service.DTOs.MeasurementUnits;
using Warehouse.Service.DTOs.Resources;
using Warehouse.Service.Exceptions;
using Warehouse.Service.Filters;
using Warehouse.Service.Services.MeasurementUnitServices;
using Warehouse.Service.Services.ResourceServices;

namespace Warehouse.Service.Services.InputDocumentServices;

public class InputDocumentService : IInputDocumentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<InputDocument> _inputDocumentRepository;
    private readonly IRepository<InputResource> _inputResourceRepository;
    private readonly IResourceService _resourceService;
    private readonly IMeasurementUnitService _measurementUnitService;

    public InputDocumentService(IMapper mapper,
        IRepository<InputDocument> inputDocumentRepository,
        IRepository<InputResource> inputResourceRepository,
        IResourceService resourceService,
        IMeasurementUnitService measurementUnitService)
    {
        _mapper = mapper;
        _inputDocumentRepository = inputDocumentRepository;
        _inputResourceRepository = inputResourceRepository;
        _resourceService = resourceService;
        _measurementUnitService = measurementUnitService;
    }

    public async Task<InputDocumentResultDto> CreateAsync(InputDocumentCreationDto creationDto)
    {
        var inputDocument = await _inputDocumentRepository
            .SelectAsync(d => d.Number == creationDto.Number);

        if (inputDocument is not null)
            throw new WarehouseException(409, $"Поступления \"{inputDocument.Number}\" уже существует");

        foreach(var inputResource in creationDto.InputResources)
        {
            var resource = await _resourceService.GetAsync(inputResource.ResourceId);

            if (resource.Status == EntityStatus.InActive)
                throw new WarehouseException(409, $"Ресурс \"{resource.Name}\" в архиве");

            var measurementUnit = await _measurementUnitService.GetAsync(inputResource.MeasurementUnitId);

            if (resource.Status == EntityStatus.InActive)
                throw new WarehouseException(409, $"Единица измерения \"{measurementUnit.Name}\" в архиве");
        }

        var mappedInputDocument = _mapper.Map<InputDocument>(creationDto);

        mappedInputDocument.InputResources = _mapper.Map<List<InputResource>>(creationDto.InputResources);

        var inputDocumentEntity = await _inputDocumentRepository.InsertAsync(mappedInputDocument);

        await _inputDocumentRepository.SaveAsync();

        return await GetAsync(inputDocumentEntity.Id);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var inputDocument = await _inputDocumentRepository.SelectAsync(r => r.Id == id);

        if (inputDocument is null)
            throw new WarehouseException(404, "Поступления не найден");

        var inputResources = await _inputResourceRepository.SelectAll(r => r.InputDocumentId == id).ToListAsync();

        foreach (var inputResource in inputResources)
        {
            var inputResourceIsDeleted = await _inputResourceRepository.DeleteAsync(r => r.Id == inputResource.Id);

            if (!inputResourceIsDeleted)
                return false;
        }

        var isDeleted = await _inputDocumentRepository.DeleteAsync(r => r.Id == id);

        await _inputDocumentRepository.SaveAsync();

        return isDeleted;
    }

    public async Task<List<InputDocumentResultDto>> GetAllAsync(InputDocumentFilter filter)
    {
        var inputDocumentsQuery = _inputDocumentRepository.SelectAll();

        if (filter.From is not null)
            inputDocumentsQuery = inputDocumentsQuery.Where(d => d.Date >= filter.From);

        if (filter.To is not null)
            inputDocumentsQuery = inputDocumentsQuery.Where(d => d.Date <= filter.To);

        if (filter.InputDocumentIds?.Any() == true)
            inputDocumentsQuery = inputDocumentsQuery
                .Where(d => filter.InputDocumentIds.Contains(d.Id));

        if (filter.ResourceIds?.Any() == true)
            inputDocumentsQuery = inputDocumentsQuery
                .Where(d => d.InputResources.Any(r => filter.ResourceIds.Contains(r.ResourceId)));

        if (filter.MeasurementIds?.Any() == true)
            inputDocumentsQuery = inputDocumentsQuery
                .Where(d => d.InputResources.Any(r => filter.MeasurementIds.Contains(r.MeasurementUnitId)));

        var inputDocuments = await inputDocumentsQuery
            .OrderByDescending(d => d.Id)
            .Include(d => d.InputResources)
                .ThenInclude(r => r.MeasurementUnit)
            .Include(d => d.InputResources)
                .ThenInclude(r => r.Resource)
            .ToListAsync();

        var results = new List<InputDocumentResultDto>();

        foreach (var inputDocument in inputDocuments)
        {
            var result = _mapper.Map<InputDocumentResultDto>(inputDocument);
            result.InputResources = new List<InputResourceResultDto>();

            foreach (var inputResource in inputDocument.InputResources)
            {
                if (filter.ResourceIds?.Any() == true && !filter.ResourceIds.Contains(inputResource.ResourceId))
                    continue;

                if (filter.MeasurementIds?.Any() == true && !filter.MeasurementIds.Contains(inputResource.MeasurementUnitId))
                    continue;

                var inputResourceResult = _mapper.Map<InputResourceResultDto>(inputResource);
                inputResourceResult.Resource = _mapper.Map<ResourceResultDto>(inputResource.Resource);
                inputResourceResult.MeasurementUnit = _mapper.Map<MeasurementUnitResultDto>(inputResource.MeasurementUnit);

                result.InputResources.Add(inputResourceResult);
            }

            results.Add(result);
        }

        return results;
    }

    public async Task<InputDocumentResultDto> GetAsync(long id)
    {
        var inputDocument = await _inputDocumentRepository
            .SelectAll(d => d.Id == id)
            .Include(d => d.InputResources)
                .ThenInclude(r => r.MeasurementUnit)
            .Include(d => d.InputResources)
                .ThenInclude(r => r.Resource)
            .FirstOrDefaultAsync();

        if (inputDocument is null)
            throw new WarehouseException(404, "Поступления не найден");

        var result = _mapper.Map<InputDocumentResultDto>(inputDocument);
        result.InputResources = new List<InputResourceResultDto>();

        foreach (var inputResource in inputDocument.InputResources)
        {
            var inputResourceResult = _mapper.Map<InputResourceResultDto>(inputResource);
            inputResourceResult.Resource = _mapper.Map<ResourceResultDto>(inputResource.Resource);
            inputResourceResult.MeasurementUnit = _mapper.Map<MeasurementUnitResultDto>(inputResource.MeasurementUnit);

            result.InputResources.Add(inputResourceResult);
        }

        return result;
    }

    public async Task<InputDocumentResultDto> UpdateAsync(InputDocumentUpdateDto updateDto)
    {
        var inputDocument = await _inputDocumentRepository
            .SelectAll(d => d.Id == updateDto.Id)
            .Include(d => d.InputResources)
            .ThenInclude(r => r.MeasurementUnit)
            .FirstOrDefaultAsync();

        if (inputDocument is null)
            throw new WarehouseException(404, "Поступления не найден");

        foreach (var inputResource in updateDto.InputResources)
        {
            var resource = await _resourceService.GetAsync(inputResource.ResourceId);
            if (inputResource.Id == 0 && resource.Status == EntityStatus.InActive)
                throw new WarehouseException(409, $"Ресурс \"{resource.Name}\" в архиве");

            var measurementUnit = await _measurementUnitService.GetAsync(inputResource.MeasurementUnitId);
            if (inputResource.Id == 0 && resource.Status == EntityStatus.InActive)
                throw new WarehouseException(409, $"Единица измерения \"{measurementUnit.Name}\" в архиве");
        }

        var updatedInputDocument = _mapper.Map(updateDto, inputDocument);
        updatedInputDocument.UpdatedAt = DateTime.UtcNow;

        foreach (var inputResource in inputDocument.InputResources)
        {
            if (inputResource.Id == 0)
                continue;

            var inputResourceUpdateDto = updateDto.InputResources
                .FirstOrDefault(r => r.Id == inputResource.Id);

            if (inputResourceUpdateDto == null)
            {
                await _inputResourceRepository.DeleteAsync(r => r.Id == inputResource.Id);
            }
            else
            {
                _mapper.Map(inputResourceUpdateDto, inputResource);

                inputResource.UpdatedAt = DateTime.UtcNow;
            }
        }

        await _inputDocumentRepository.SaveAsync();

        return await GetAsync(updateDto.Id);
    }
}