namespace Warehouse.Service.Filters;

public class InputDocumentFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public List<long> InputDocumentIds { get; set; }
    public List<long> ResourceIds { get; set; }
    public List<long> MeasurementIds { get; set; }
}