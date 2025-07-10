using CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;

namespace CustomCADs.Identity.Endpoints.Identity.Get.MyAccount;

public sealed class MyAccountEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<MyAccountResponse>
{
	public override void Configure()
	{
		Get("my-account");
		Group<IdentityGroup>();
		Description(d => d
			.WithName(IdentityNames.MyAccount)
			.WithSummary("My Account")
			.WithDescription("See your Account's details")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		GetUserByUsernameDto user = await sender.SendQueryAsync(
			new GetUserByUsernameQuery(User.GetName()),
			ct
		).ConfigureAwait(false);

		MyAccountResponse response = new(
			Id: user.Id.Value,
			Role: user.Role,
			Username: user.Username,
			FirstName: user.FirstName,
			LastName: user.LastName,
			Email: user.Email.Value,
			CreatedAt: user.CreatedAt
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
