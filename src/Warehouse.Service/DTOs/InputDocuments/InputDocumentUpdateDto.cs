using Warehouse.Service.DTOs.InputResources;

namespace Warehouse.Service.DTOs.InputDocuments;

public class InputDocumentUpdateDto
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public List<InputResourceUpdateDto> InputResources { get; set; }
}