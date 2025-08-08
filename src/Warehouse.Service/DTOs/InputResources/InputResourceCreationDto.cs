namespace Warehouse.Service.DTOs.InputResources;

public class InputResourceCreationDto
{
    public decimal Amount { get; set; }
    public long ResourceId { get; set; }
    public long MeasurementUnitId { get; set; }
}