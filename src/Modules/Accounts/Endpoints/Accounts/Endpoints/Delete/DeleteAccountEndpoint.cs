using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Delete;

public sealed class DeleteAccountEndpoint(IRequestSender sender)
	: Endpoint<DeleteAccountRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<AccountsGroup>();
		Description(d => d
			.WithSummary("Delete")
			.WithDescription("Delete an Account")
		);
	}

	public override async Task HandleAsync(DeleteAccountRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new DeleteAccountCommand(AccountId.New(req.Id)),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
