using CustomCADs.Auth.Application.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Endpoints.SignUp.Register;

public class RegisterEndpoint(IUserService service) 
    : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("register");
        Group<SignUpGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        CreateUserDto dto = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            Password: req.Password,
            FirstName: req.FirstName,
            LastName: req.LastName
        );
        IdentityResult result = await service.CreateUserAsync(dto).ConfigureAwait(false);
        
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
