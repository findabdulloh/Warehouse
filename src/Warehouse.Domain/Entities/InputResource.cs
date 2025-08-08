using Warehouse.Domain.Commons;

namespace Warehouse.Domain.Entities;

public class InputResource : Auditable
{
    public decimal Amount { get; set; }

    public long InputDocumentId { get; set; }
    public InputDocument InputDocument { get; set; }

    public long ResourceId { get; set; }
    public Resource Resource { get; set; }

    public long MeasurementUnitId { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
}