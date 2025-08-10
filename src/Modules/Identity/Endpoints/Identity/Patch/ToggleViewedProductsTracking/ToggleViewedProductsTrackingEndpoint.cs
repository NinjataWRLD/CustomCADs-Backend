using CustomCADs.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Identity.Endpoints.Identity.Patch.ToggleViewedProductsTracking;

public sealed class ToggleViewedProductsTrackingEndpoint(IRequestSender sender)
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Patch("viewed-products");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(d => d
			.WithName(IdentityNames.ToggleViewedProductsTracking)
			.WithSummary("Viewed Products Tracking")
			.WithDescription("Toggle whether the Products you View get Tracked")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new ToggleViewedProductsTrackingCommand(
				Username: User.GetName()
			),
			ct
		).ConfigureAwait(false);
	}
}
