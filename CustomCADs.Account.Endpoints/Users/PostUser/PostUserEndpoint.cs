using CustomCADs.Account.Application.Roles.Queries.ExistsByName;
using CustomCADs.Account.Application.Roles.Queries.GetAllNames;
using CustomCADs.Account.Application.Users.Commands.Create;
using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Account.Endpoints.Users.GetUser;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Account.Endpoints.Users.PostUser;

using static Helpers.ApiMessages;

public class PostUserEndpoint(IMediator mediator) : Endpoint<PostUserRequest, UserResponse>
{
    public override void Configure()
    {
        Post("");
        Group<UsersGroup>();
    }

    public override async Task HandleAsync(PostUserRequest req, CancellationToken ct)
    {
        RoleExistsByNameQuery existsByNameQuery = new(req.Role);
        bool roleExists = await mediator.Send(existsByNameQuery, ct).ConfigureAwait(false);

        if (!roleExists)
        {
            GetAllRoleNamesQuery getRoleNamesQuery = new();
            IEnumerable<string> roleNames = await mediator.Send(getRoleNamesQuery, ct).ConfigureAwait(false);

            ValidationFailures.Add(new("Role", InvalidRole, req.Role)
            {
                FormattedMessagePlaceholderValues = new() { ["roles"] = string.Join(", ", roleNames) },
            });
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        CreateUserCommand command = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            Password: req.Password,
            FirstName: req.FirstName,
            LastName: req.LastName
        );
        Guid id = await mediator.Send(command, ct).ConfigureAwait(false);

        GetUserByIdQuery getByIdQuery = new(id);
        GetUserByIdDto addedUser = await mediator.Send(getByIdQuery, ct).ConfigureAwait(false);

        UserResponse response = new(
            Email: addedUser.Email,
            Username: addedUser.Username,
            Role: addedUser.Role,
            FirstName: addedUser.FirstName,
            LastName: addedUser.LastName
        );
        await SendCreatedAtAsync<GetUserEndpoint>(new { req.Username }, response).ConfigureAwait(false);
    }
}
