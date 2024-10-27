using FastEndpoints;

namespace CustomCADs.Account.Endpoints.Users.GetUser;

public class GetUserRequest
{
    [BindFrom("username")]
    public required string Username { get; set; }
}
