using CustomCADs.Account.Application.Roles.Commands.DeleteByName;
using CustomCADs.Shared.Core.Events;
using FastEndpoints;
using MediatR;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Roles.DeleteRole;
public class DeleteRoleEndpoint(IMediator mediator, IMessageBus bus) : Endpoint<DeleteRoleRequest>
{
    public override void Configure()
    {
        Delete("{name}");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(DeleteRoleRequest req, CancellationToken ct)
    {
        DeleteRoleByNameCommand command = new(req.Name);
        await mediator.Send(command, ct).ConfigureAwait(false);
        
        RoleDeletedEvent @event = new() { Name = req.Name };
        await bus.PublishAsync(@event).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
