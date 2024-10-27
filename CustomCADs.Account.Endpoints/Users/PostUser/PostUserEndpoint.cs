using CustomCADs.Account.Application.Roles.Queries.ExistsByName;
using CustomCADs.Account.Application.Roles.Queries.GetAllNames;
using CustomCADs.Account.Application.Users.Commands.Create;
using CustomCADs.Account.Application.Users.Common.Dtos;
using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Account.Endpoints.Users.GetUser;
using CustomCADs.Shared.Events.Events;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Users.PostUser;

using static Helpers.ApiMessages;

public class PostUserEndpoint(IMessageBus bus) : Endpoint<PostUserRequest, UserResponseDto>
{
    public override void Configure()
    {
        Post("");
        Group<UsersGroup>();
    }

    public override async Task HandleAsync(PostUserRequest req, CancellationToken ct)
    {
        RoleExistsByNameQuery existsByNameQuery = new(req.Role);
        var roleExists = await bus.InvokeAsync<bool>(existsByNameQuery, ct).ConfigureAwait(false);

        if (!roleExists)
        {
            GetAllRoleNamesQuery getRoleNamesQuery = new();
            var roleNames = await bus.InvokeAsync<IEnumerable<string>>(getRoleNamesQuery, ct).ConfigureAwait(false);

            ValidationFailures.Add(new()
            {
                ErrorMessage = InvalidRole,
                FormattedMessagePlaceholderValues = new() { ["roles"] = string.Join(", ", roleNames) },
            });
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        UserCreatedEvent @event = new()
        {
            Role = req.Role,
            Username = req.Username,
            Email = req.Email,
            Password = req.Password,
        };
        await bus.PublishAsync(@event).ConfigureAwait(false);
        
        CreateUserDto dto = new()
        {
            Email = req.Email,
            Username = req.Username,
            RoleName = req.Role,
            NameInfo = new()
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
            },
        };
        var id = await bus.InvokeAsync<Guid>(new CreateUserCommand(dto), ct).ConfigureAwait(false);

        GetUserByIdQuery query = new(id);
        var addedUser = await bus.InvokeAsync<GetUserByIdDto>(query, ct).ConfigureAwait(false);

        UserResponseDto response = new()
        {
            Email = addedUser.Email,
            Username = addedUser.Username,
            Role = addedUser.Role,
            FirstName = addedUser.FirstName,
            LastName = addedUser.LastName,
        };
        await SendCreatedAtAsync<GetUserEndpoint>(new { req.Username }, response).ConfigureAwait(false);
    }
}
