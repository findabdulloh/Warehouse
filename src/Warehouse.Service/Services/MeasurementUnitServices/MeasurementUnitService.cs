using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data.IRepositories;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Service.DTOs.MeasurementUnits;
using Warehouse.Service.DTOs.Resources;
using Warehouse.Service.Exceptions;

namespace Warehouse.Service.Services.MeasurementUnitServices;

public class MeasurementUnitService : IMeasurementUnitService
{
    private readonly IMapper _mapper;
    private readonly IRepository<MeasurementUnit> _measurementUnitRepository;

    public MeasurementUnitService(IRepository<MeasurementUnit> measurementUnitRepository, IMapper mapper)
    {
        _mapper = mapper;
        _measurementUnitRepository = measurementUnitRepository;
    }

    public async Task<MeasurementUnitResultDto> CreateAsync(MeasurementUnitCreationDto creationDto)
    {
        var measurementUnit = await _measurementUnitRepository.SelectAsync(r => r.Name == creationDto.Name);

        if (measurementUnit is not null)
            throw new WarehouseException(409, $"Единица измерения \"{measurementUnit.Name}\" уже существует");

        var mappedMeasurementUnit = _mapper.Map<MeasurementUnit>(creationDto);

        var measurementUnitEntity = await _measurementUnitRepository.InsertAsync(mappedMeasurementUnit);

        await _measurementUnitRepository.SaveAsync();

        return _mapper.Map<MeasurementUnitResultDto>(measurementUnitEntity);
    }

    public async Task<MeasurementUnitResultDto> ChangeStatusAsync(long id, bool isActive)
    {
        var measurementUnit = await _measurementUnitRepository.SelectAsync(r => r.Id == id);

        if (measurementUnit is null)
            throw new WarehouseException(404, $"Единица измерения не найден");

        measurementUnit.Status = isActive ? EntityStatus.Active : EntityStatus.InActive;

        var updatedEntity = _measurementUnitRepository.Update(measurementUnit);

        await _measurementUnitRepository.SaveAsync();

        return _mapper.Map<MeasurementUnitResultDto>(updatedEntity);
    }
    
    public async Task<bool> DeleteAsync(long id)
    {
        var measurementUnit = await _measurementUnitRepository.SelectAsync(r => r.Id == id);

        if (measurementUnit is null)
            throw new WarehouseException(404, $"Единица измерения не найден");

        var isDeleted = await _measurementUnitRepository.DeleteAsync(r => r.Id == id);

        await _measurementUnitRepository.SaveAsync();

        return isDeleted;
    }

    public async Task<List<MeasurementUnitResultDto>> GetAllAsync(bool? isActive)
    {
        var measurementUnitsQuery = _measurementUnitRepository.SelectAll();

        if (isActive is not null)
            measurementUnitsQuery = _measurementUnitRepository.SelectAll(r => isActive == true ? r.Status == EntityStatus.Active : r.Status == EntityStatus.InActive);

        var measurementUnits = await measurementUnitsQuery.OrderByDescending(m => m.Id).ToListAsync();

        return _mapper.Map<List<MeasurementUnitResultDto>>(measurementUnits);
    }

    public async Task<MeasurementUnitResultDto> GetAsync(long id)
    {
        var measurementUnit = await _measurementUnitRepository.SelectAsync(r => r.Id == id);

        if (measurementUnit is null)
            throw new WarehouseException(404, $"Единица измерения не найден");

        return _mapper.Map<MeasurementUnitResultDto>(measurementUnit);
    }

    public async Task<MeasurementUnitResultDto> UpdateAsync(MeasurementUnitUpdateDto updateDto)
    {
        var measurementUnit = await _measurementUnitRepository.SelectAsync(r => r.Id == updateDto.Id);

        if (measurementUnit is null)
            throw new WarehouseException(404, $"Единица измерения не найден");

        var updatedMeasurementUnit = _mapper.Map(updateDto, measurementUnit);
        updatedMeasurementUnit.UpdatedAt = DateTime.UtcNow;

        var updatedEntity = _measurementUnitRepository.Update(updatedMeasurementUnit);

        await _measurementUnitRepository.SaveAsync();

        return _mapper.Map<MeasurementUnitResultDto>(updatedEntity);
    }
}