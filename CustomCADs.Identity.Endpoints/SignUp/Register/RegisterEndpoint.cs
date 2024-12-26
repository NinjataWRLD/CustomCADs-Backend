using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Endpoints.SignUp.Register;

public sealed class RegisterEndpoint(IUserService service)
    : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("register");
        Group<SignUpGroup>();
        Description(d => d
            .WithSummary("01. Register")
            .WithDescription("Register by providing a Role, Username, Email, Password, and Time zone, and optional First and Last names, and receive an email from which to verify your Email")
        );
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        CreateUserDto dto = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            TimeZone: req.TimeZone,
            Password: req.Password,
            FirstName: req.FirstName,
            LastName: req.LastName
        );
        IdentityResult result = await service.CreateAsync(dto).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            ValidationFailures.AddRange(result.Errors
                .Select(e => new ValidationFailure(e.Code, e.Description)));

            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }
        await service.SendVerificationEmailAsync(req.Username).ConfigureAwait(false);

        await SendOkAsync().ConfigureAwait(false);
    }
}
