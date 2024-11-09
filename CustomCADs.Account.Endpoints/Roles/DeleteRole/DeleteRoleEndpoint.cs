using CustomCADs.Account.Application.Roles.Commands.DeleteByName;

namespace CustomCADs.Account.Endpoints.Roles.DeleteRole;

public class DeleteRoleEndpoint(IRequestSender sender)
    : Endpoint<DeleteRoleRequest>
{
    public override void Configure()
    {
        Delete("{name}");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(DeleteRoleRequest req, CancellationToken ct)
    {
        DeleteRoleByNameCommand command = new(req.Name);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
