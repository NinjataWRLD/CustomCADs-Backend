using CustomCADs.Accounts.Application.Accounts.Commands.DeleteByName;

namespace CustomCADs.Accounts.Endpoints.Accounts.Delete;

public class DeleteAccountEndpoint(IRequestSender sender)
    : Endpoint<DeleteAccountRequest>
{
    public override void Configure()
    {
        Delete("{username}");
        Group<AccountsGroup>();
        Description(d => d
            .WithSummary("4. Delete")
            .WithDescription("Delete an Account by specifying its Username")
        );
    }

    public override async Task HandleAsync(DeleteAccountRequest req, CancellationToken ct)
    {
        DeleteAccountByNameCommand command = new(req.Username);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
