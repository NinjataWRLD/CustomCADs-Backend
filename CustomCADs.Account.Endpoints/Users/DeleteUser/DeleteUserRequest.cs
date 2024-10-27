using FastEndpoints;

namespace CustomCADs.Account.Endpoints.Users.DeleteUser;

public class DeleteUserRequest
{
    [BindFrom("username")]
    public required string Username { get; set; }
}
