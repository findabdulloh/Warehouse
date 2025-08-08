using Warehouse.Service.DTOs.MeasurementUnits;
using Warehouse.Service.DTOs.Resources;

namespace Warehouse.Service.DTOs.InputResources;

public class InputResourceResultDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal Amount { get; set; }
    public long InputDocumentId { get; set; }
    public long ResourceId { get; set; }
    public ResourceResultDto Resource { get; set; }
    public long MeasurementUnitId { get; set; }
    public MeasurementUnitResultDto MeasurementUnit { get; set; }
}