namespace Warehouse.Service.DTOs.InputResources;

public class InputResourceUpdateDto
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public long ResourceId { get; set; }
    public long MeasurementUnitId { get; set; }
}