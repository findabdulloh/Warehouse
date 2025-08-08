using Warehouse.Domain.Commons;

namespace Warehouse.Domain.Entities;

public class InputDocument : Auditable
{
    public string Number { get; set; }
    public DateTime Date { get; set; }
    
    public ICollection<InputResource> InputResources { get; set; }
}