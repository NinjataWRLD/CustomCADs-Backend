using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Register;

public sealed class RegisterEndpoint(IUserService service, LinkGenerator links)
    : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("register");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.Register)
            .WithSummary("Register")
            .WithDescription("Register an Account")
        );
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        await service.RegisterAsync(
            dto: new(
                Role: req.Role,
                Username: req.Username,
                Email: req.Email,
                Password: req.Password
            ),
            timeZone: req.TimeZone,
            firstName: req.FirstName,
            lastName: req.LastName
        ).ConfigureAwait(false);

        await service.SendVerificationEmailAsync(
            username: req.Username,
            getUri: ect => links.GetUriByName(
                httpContext: HttpContext,
                endpointName: IdentityNames.ConfirmEmail,
                values: new { username = req.Username, token = ect }
            ) ?? throw new InvalidOperationException("Unable to generate confirmation link.")
        ).ConfigureAwait(false);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
