using CustomCADs.Account.Application.Users.Commands.DeleteByName;
using CustomCADs.Shared.Events.Events;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Users.DeleteUser;

public class DeleteUserEndpoint(IMessageBus bus) : Endpoint<DeleteUserRequest>
{
    public override void Configure()
    {
        Delete("{username}");
        Group<UsersGroup>();
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        DeleteUserByNameCommand command = new(req.Username);
        await bus.InvokeAsync(command, ct).ConfigureAwait(false);

        UserDeletedEvent @event = new() { Username = req.Username };
        await bus.PublishAsync(@event).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
