using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Create;
using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetByUsername;
using CustomCADs.Accounts.Endpoints.Accounts.Dtos;
using CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Get.Single;

namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Post;

public sealed class PostAccountEndpoint(IRequestSender sender)
	: Endpoint<PostAccountRequest, AccountResponse>
{
	public override void Configure()
	{
		Post("");
		Group<AccountsGroup>();
		Description(d => d
			.WithSummary("Create")
			.WithDescription("Create an Account")
		);
	}

	public override async Task HandleAsync(PostAccountRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new CreateAccountCommand(
				Role: req.Role,
				Username: req.Username,
				Email: req.Email,
				Password: req.Password,
				FirstName: req.FirstName,
				LastName: req.LastName
			),
			ct
		).ConfigureAwait(false);

		var newAccount = await sender.SendQueryAsync(
			new GetAccountByUsernameQuery(req.Username)
		, ct).ConfigureAwait(false);

		AccountResponse response = newAccount.ToResponse();
		await SendCreatedAtAsync<GetAccountEndpoint>(new { req.Username }, response).ConfigureAwait(false);
	}
}
