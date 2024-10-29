using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth.RetryVerifyEmail;

public class RetryVerifyEmailRequest
{
    [BindFrom("username")]
    public required string Username { get; set; }   
}
