using CustomCADs.Account.Application.Users.Commands.DeleteByName;

namespace CustomCADs.Account.Endpoints.Users.DeleteUser;

public class DeleteUserEndpoint(IMediator mediator) : Endpoint<DeleteUserRequest>
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

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
