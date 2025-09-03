using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Identity.Endpoints.Identity.Get.Authorization;

public sealed class AuthorizationEndpoint
	: EndpointWithoutRequest
{
	public override void Configure()
	{
		Get("authorization");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(d => d
			.WithName(IdentityNames.Authorization)
			.WithSummary("AuthZ")
			.WithDescription("See what Role you're logged in with")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await Send.OkAsync(User.GetAuthorization()).ConfigureAwait(false);
	}
}
