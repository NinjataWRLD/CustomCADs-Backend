using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.VerifyEmail;

public class VerifyEmailRequest
{
    [BindFrom("username")]
    public required string Username { get; set; }
    
    public string? Token { get; set; }
}
