using CustomCADs.Account.Application.Users.Commands.Create;
using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Account.Endpoints.Users.Get.Single;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Endpoints.Users.Post;

public class PostUserEndpoint(IRequestSender sender)
    : Endpoint<PostUserRequest, UserResponse>
{
    public override void Configure()
    {
        Post("");
        Group<UsersGroup>();
        Description(d => d.WithSummary("2. I want to create a User"));
    }

    public override async Task HandleAsync(PostUserRequest req, CancellationToken ct)
    {
        CreateUserCommand command = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            Password: req.Password,
            FirstName: req.FirstName,
            LastName: req.LastName
        );
        UserId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetUserByIdQuery getByIdQuery = new(id);
        GetUserByIdDto addedUser = await sender.SendQueryAsync(getByIdQuery, ct).ConfigureAwait(false);

        UserResponse response = addedUser.ToUserResponse();
        await SendCreatedAtAsync<GetUserEndpoint>(new { req.Username }, response).ConfigureAwait(false);
    }
}
