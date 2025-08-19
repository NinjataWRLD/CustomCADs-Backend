using Refit;

namespace CustomCADs.Speedy.Http.Endpoints.ServicesEndpoints;

using DestinationServices;
using Services;

internal interface IServicesEndpoints
{
	[Post("/")]
	Task<ServicesResponse> Services(ServicesRequest request, CancellationToken ct = default);

	[Post("/destination")]
	Task<DestinationServicesResponse> DestinationServices(DestinationServicesRequest request, CancellationToken ct = default);
}
