using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.UserExists;

public class UserExistsRequest
{
    [BindFrom("username")]
    public required string Username { get; set; }
}
