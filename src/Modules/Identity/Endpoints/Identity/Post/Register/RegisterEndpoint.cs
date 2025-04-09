using CustomCADs.Identity.Application.Users.Commands.Internal.Register;
using CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;
using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Register;

public sealed class RegisterEndpoint(IRequestSender sender, LinkGenerator links)
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
        await sender.SendCommandAsync(
            new RegisterUserCommand(
                Role: req.Role,
                Username: req.Username,
                Email: req.Email,
                Password: req.Password,
                TimeZone: req.TimeZone,
                FirstName: req.FirstName,
                LastName: req.LastName
            ),
            ct
        ).ConfigureAwait(false);

        await sender.SendCommandAsync(
            new VerificationEmailCommand(
                Username: req.Username,
                GetUri: ect => links.GetUriByName(
                    httpContext: HttpContext,
                    endpointName: IdentityNames.ConfirmEmail,
                    values: new { username = req.Username, token = ect }
                ) ?? throw new InvalidOperationException("Unable to generate confirmation link.")
            ),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
