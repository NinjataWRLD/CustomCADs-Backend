using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Identity.Endpoints.Identity.Get.Authentication;

public sealed class AuthenticationEndpoint
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Get("authentication");
		Group<IdentityGroup>();
		Description(d => d
			.WithName(IdentityNames.Authentication)
			.WithSummary("AuthN")
			.WithDescription("See if you're logged in")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await Send.OkAsync(User.GetAuthentication()).ConfigureAwait(false);
	}
}
