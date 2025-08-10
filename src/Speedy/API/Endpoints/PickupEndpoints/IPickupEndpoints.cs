using Refit;

namespace CustomCADs.Speedy.API.Endpoints.PickupEndpoints;

using Pickup;
using PickupTerms;

public interface IPickupEndpoints
{
	[Post("/")]
	Task<PickupResponse> Pickup(PickupRequest request, CancellationToken ct = default);

	[Post("/")]
	Task<PickupTermsResponse> PickupTerms(PickupTermsRequest request, CancellationToken ct = default);
}
