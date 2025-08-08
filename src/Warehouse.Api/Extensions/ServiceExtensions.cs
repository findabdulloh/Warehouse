using Warehouse.Data.IRepositories;
using Warehouse.Data.Repositories;
using Warehouse.Service.Services.InputDocumentServices;
using Warehouse.Service.Services.MeasurementUnitServices;
using Warehouse.Service.Services.ResourceServices;

namespace Warehouse.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IResourceService, ResourceService>();

        services.AddScoped<IMeasurementUnitService, MeasurementUnitService>();
        
        services.AddScoped<IInputDocumentService, InputDocumentService>();
    }
}