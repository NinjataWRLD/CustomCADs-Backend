﻿using CustomCADs.Account.Application.Users.Commands.DeleteByName;

namespace CustomCADs.Account.Endpoints.Users.Delete;

public class DeleteUserEndpoint(IRequestSender sender)
    : Endpoint<DeleteUserRequest>
{
    public override void Configure()
    {
        Delete("{username}");
        Group<UsersGroup>();
        Description(d => d.WithSummary("4. I want to delete a User"));
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        DeleteUserByNameCommand command = new(req.Username);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
