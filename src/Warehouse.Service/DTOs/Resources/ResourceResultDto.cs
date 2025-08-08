using Warehouse.Domain.Enums;

namespace Warehouse.Service.DTOs.Resources;

public class ResourceResultDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Name { get; set; }
    public EntityStatus Status { get; set; }
}