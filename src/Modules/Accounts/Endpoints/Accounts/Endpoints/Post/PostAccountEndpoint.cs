using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Create;
using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;
using CustomCADs.Accounts.Endpoints.Accounts.Dtos;
using CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Get.Single;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

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
		AccountId id = await sender.SendCommandAsync(
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

		GetAccountByIdDto newAccount = await sender.SendQueryAsync(
			new GetAccountByIdQuery(id)
		, ct).ConfigureAwait(false);

		AccountResponse response = newAccount.ToResponse();
		await Send.CreatedAtAsync<GetAccountEndpoint>(new { id }, response).ConfigureAwait(false);
	}
}
