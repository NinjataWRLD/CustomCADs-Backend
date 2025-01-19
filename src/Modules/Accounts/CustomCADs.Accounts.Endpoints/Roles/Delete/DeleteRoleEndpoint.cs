using CustomCADs.Accounts.Application.Roles.Commands.Delete;

namespace CustomCADs.Accounts.Endpoints.Roles.Delete;

public sealed class DeleteRoleEndpoint(IRequestSender sender)
    : Endpoint<DeleteRoleRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<RolesGroup>();
        Description(d => d
            .WithSummary("4. Delete")
            .WithDescription("Delete a Role")
        );
    }

    public override async Task HandleAsync(DeleteRoleRequest req, CancellationToken ct)
    {
        DeleteRoleCommand command = new(req.Name);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
