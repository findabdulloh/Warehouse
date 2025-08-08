using Warehouse.Domain.Commons;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities;

public class MeasurementUnit : Auditable
{
    public string Name { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;
}