using Warehouse.Service.DTOs.InputResources;

namespace Warehouse.Service.DTOs.InputDocuments;

public class InputDocumentResultDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public List<InputResourceResultDto> InputResources { get; set; }
}