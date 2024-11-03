using CustomCADs.Account.Application.Users.Commands.DeleteByName;
using CustomCADs.Shared.Core.Events;
using FastEndpoints;
using MediatR;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Users.DeleteUser;

public class DeleteUserEndpoint(IMediator mediator, IMessageBus bus) : Endpoint<DeleteUserRequest>
{
    public override void Configure()
    {
        Delete("{username}");
        Group<UsersGroup>();
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        DeleteUserByNameCommand command = new(req.Username);
        await mediator.Send(command, ct).ConfigureAwait(false);

        UserDeletedEvent @event = new() { Username = req.Username };
        await bus.PublishAsync(@event).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
