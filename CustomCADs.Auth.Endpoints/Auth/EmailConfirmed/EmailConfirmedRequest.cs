using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.EmailConfirmed;

public class EmailConfirmedRequest
{
    [BindFrom("username")]
    public required string Username { get; set; }
}
