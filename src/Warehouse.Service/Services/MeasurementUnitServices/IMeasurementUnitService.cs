using Warehouse.Service.DTOs.MeasurementUnits;

namespace Warehouse.Service.Services.MeasurementUnitServices;

public interface IMeasurementUnitService
{
    public Task<MeasurementUnitResultDto> CreateAsync(MeasurementUnitCreationDto creationDto);
    public Task<MeasurementUnitResultDto> UpdateAsync(MeasurementUnitUpdateDto updateDto);
    public Task<bool> DeleteAsync(long id);
    public Task<MeasurementUnitResultDto> ChangeStatusAsync(long id, bool isActive);
    public Task<MeasurementUnitResultDto> GetAsync(long id);
    public Task<List<MeasurementUnitResultDto>> GetAllAsync(bool? isActive);
}