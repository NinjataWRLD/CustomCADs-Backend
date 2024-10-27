using CustomCADs.Account.Application.Roles.Commands.DeleteByName;
using CustomCADs.Shared.Events.Events;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Roles.DeleteRole;
public class DeleteRoleEndpoint(IMessageBus bus) : Endpoint<DeleteRoleRequest>
{
    public override void Configure()
    {
        Delete("{name}");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(DeleteRoleRequest req, CancellationToken ct)
    {
        RoleDeletedEvent @event = new() { Name = req.Name };
        await bus.PublishAsync(@event).ConfigureAwait(false);

        DeleteRoleByNameCommand command = new(req.Name);
        await bus.InvokeAsync(command, ct).ConfigureAwait(false);
        
        await SendNoContentAsync().ConfigureAwait(false);
    }
}
