using CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;

namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Delete;

public sealed class DeleteAccountEndpoint(IRequestSender sender)
    : Endpoint<DeleteAccountRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<AccountsGroup>();
        Description(d => d
            .WithSummary("4. Delete")
            .WithDescription("Delete an Account")
        );
    }

    public override async Task HandleAsync(DeleteAccountRequest req, CancellationToken ct)
    {
        DeleteAccountCommand command = new(req.Username);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
