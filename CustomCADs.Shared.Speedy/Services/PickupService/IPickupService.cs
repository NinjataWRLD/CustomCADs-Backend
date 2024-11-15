using Refit;

namespace CustomCADs.Shared.Speedy.Services.PickupService;

using Pickup;
using PickupTerms;

public interface IPickupService
{
    [Post("")]
    Task<PickupResponse> Pickup(PickupRequest request, CancellationToken ct = default);

    [Post("")]
    Task<PickupTermsResponse> PickupTerms(PickupTermsRequest request, CancellationToken ct = default);
}
