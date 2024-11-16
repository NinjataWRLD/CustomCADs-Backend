using Refit;

namespace CustomCADs.Shared.Speedy.Services.ServicesService;

using DestinationServices;
using Services;

public interface IServicesService
{
    [Post("")]
    Task<ServicesResponse> Services(ServicesRequest request, CancellationToken ct = default);

    [Post("destination")]
    Task<DestinationServicesResponse> DestinationServices(DestinationServicesRequest request, CancellationToken ct = default);
}
