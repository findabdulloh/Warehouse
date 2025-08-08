using AutoMapper;
using Warehouse.Domain.Entities;
using Warehouse.Service.DTOs.InputDocuments;
using Warehouse.Service.DTOs.InputResources;
using Warehouse.Service.DTOs.MeasurementUnits;
using Warehouse.Service.DTOs.Resources;

namespace Warehouse.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<InputDocument, InputDocumentCreationDto>().ReverseMap();
        CreateMap<InputDocument, InputDocumentUpdateDto>().ReverseMap();
        CreateMap<InputDocument, InputDocumentResultDto>().ReverseMap();

        CreateMap<InputResource, InputResourceCreationDto>().ReverseMap();
        CreateMap<InputResource, InputResourceUpdateDto>().ReverseMap();
        CreateMap<InputResource, InputResourceResultDto>().ReverseMap();

        CreateMap<MeasurementUnit, MeasurementUnitCreationDto>().ReverseMap();
        CreateMap<MeasurementUnit, MeasurementUnitUpdateDto>().ReverseMap();
        CreateMap<MeasurementUnit, MeasurementUnitResultDto>().ReverseMap();

        CreateMap<Resource, ResourceCreationDto>().ReverseMap();
        CreateMap<Resource, ResourceUpdateDto>().ReverseMap();
        CreateMap<Resource, ResourceResultDto>().ReverseMap();
    }
}