using Warehouse.Service.DTOs.InputResources;

namespace Warehouse.Service.DTOs.InputDocuments;

public class InputDocumentCreationDto
{
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public List<InputResourceCreationDto> InputResources { get; set; }
}