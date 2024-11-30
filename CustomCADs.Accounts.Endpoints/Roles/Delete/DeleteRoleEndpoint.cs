using CustomCADs.Accounts.Application.Roles.Commands.DeleteByName;

namespace CustomCADs.Accounts.Endpoints.Roles.Delete;

public class DeleteRoleEndpoint(IRequestSender sender)
    : Endpoint<DeleteRoleRequest>
{
    public override void Configure()
    {
        Delete("{name}");
        Group<RolesGroup>();
        Description(d => d.WithSummary("4. I want to delete a Role"));
    }

    public override async Task HandleAsync(DeleteRoleRequest req, CancellationToken ct)
    {
        DeleteRoleByNameCommand command = new(req.Name);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
