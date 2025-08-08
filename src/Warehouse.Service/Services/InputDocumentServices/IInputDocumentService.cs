using Warehouse.Service.DTOs.InputDocuments;
using Warehouse.Service.Filters;

namespace Warehouse.Service.Services.InputDocumentServices;

public interface IInputDocumentService
{
    public Task<InputDocumentResultDto> CreateAsync(InputDocumentCreationDto creationDto);
    public Task<InputDocumentResultDto> UpdateAsync(InputDocumentUpdateDto updateDto);
    public Task<bool> DeleteAsync(long id);
    public Task<InputDocumentResultDto> GetAsync(long id);
    public Task<List<InputDocumentResultDto>> GetAllAsync(InputDocumentFilter filter);
}