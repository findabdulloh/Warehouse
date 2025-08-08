using Microsoft.AspNetCore.Mvc;
using Warehouse.Service.DTOs.MeasurementUnits;
using Warehouse.Service.DTOs.Resources;
using Warehouse.Service.Services.MeasurementUnitServices;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementUnitsController
{
    private readonly IMeasurementUnitService _measurementUnitsService;

    public MeasurementUnitsController(IMeasurementUnitService measurementUnitService)
    {
        _measurementUnitsService = measurementUnitService;
    }

    [HttpGet("{id}")]
    public async Task<MeasurementUnitResultDto> GetAsync(long id)
    {
        return await _measurementUnitsService.GetAsync(id);
    }

    [HttpGet]
    public async Task<List<MeasurementUnitResultDto>> GetAllAsync(bool? isActive)
    {
        return await _measurementUnitsService.GetAllAsync(isActive);
    }

    [HttpPost]
    public async Task<MeasurementUnitResultDto> PostAsync(MeasurementUnitCreationDto dto)
    {
        return await _measurementUnitsService.CreateAsync(dto);
    }

    [HttpPut]
    public async Task<MeasurementUnitResultDto> PutAsync(MeasurementUnitUpdateDto dto)
    {
        return await _measurementUnitsService.UpdateAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task<MeasurementUnitResultDto> PutAsync(long id, bool isActive)
    {
        return await _measurementUnitsService.ChangeStatusAsync(id, isActive);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(long id)
    {
        return await _measurementUnitsService.DeleteAsync(id);
    }
}