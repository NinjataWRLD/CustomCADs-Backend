namespace CustomCADs.Auth.Endpoints.SignIn.ForgotPassword;

public class ForgotPasswordEndpoint(IUserService service) 
    : Endpoint<ForgotPasswordRequest>
{
    public override void Configure()
    {
        Get("forgotPassword");
        Group<SignInGroup>();
    }

    public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
    {
        await service.SendResetPasswordEmailAsync(req.Email).ConfigureAwait(false);
        
        await SendOkAsync("Check your email!").ConfigureAwait(false);
    }
}
