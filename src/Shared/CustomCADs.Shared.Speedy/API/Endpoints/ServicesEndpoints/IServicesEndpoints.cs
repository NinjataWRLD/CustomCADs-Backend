using Refit;

namespace CustomCADs.Shared.Speedy.API.Endpoints.ServicesEndpoints;

using DestinationServices;
using Services;

public interface IServicesEndpoints
{
    [Post("/")]
    Task<ServicesResponse> Services(ServicesRequest request, CancellationToken ct = default);

    [Post("/destination")]
    Task<DestinationServicesResponse> DestinationServices(DestinationServicesRequest request, CancellationToken ct = default);
}
