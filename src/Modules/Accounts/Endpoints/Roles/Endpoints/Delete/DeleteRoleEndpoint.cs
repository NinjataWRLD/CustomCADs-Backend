﻿using CustomCADs.Accounts.Application.Roles.Commands.Internal.Delete;

namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints.Delete;

public sealed class DeleteRoleEndpoint(IRequestSender sender)
    : Endpoint<DeleteRoleRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<RolesGroup>();
        Description(d => d
            .WithSummary("Delete")
            .WithDescription("Delete a Role")
        );
    }

    public override async Task HandleAsync(DeleteRoleRequest req, CancellationToken ct)
    {
        await sender.SendCommandAsync(
            new DeleteRoleCommand(req.Name),
            ct
        ).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
