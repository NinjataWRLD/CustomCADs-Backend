using Refit;

namespace CustomCADs.Speedy.Http.Endpoints.PickupEndpoints;

using Pickup;
using PickupTerms;

internal interface IPickupEndpoints
{
	[Post("/")]
	Task<PickupResponse> Pickup(PickupRequest request, CancellationToken ct = default);

	[Post("/")]
	Task<PickupTermsResponse> PickupTerms(PickupTermsRequest request, CancellationToken ct = default);
}
