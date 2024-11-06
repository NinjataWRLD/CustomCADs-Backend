using CustomCADs.Account.Application.Roles.Commands.DeleteByName;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Account.Endpoints.Roles.DeleteRole;
public class DeleteRoleEndpoint(IMediator mediator) : Endpoint<DeleteRoleRequest>
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

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
