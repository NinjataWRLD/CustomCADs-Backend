using CustomCADs.Identity.Domain;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Register;

using static AccountConstants;
using static Constants;
using static Constants.FluentMessages;
using static Constants.Roles;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        string[] roles = [Client, Contributor];

        RuleFor(r => r.Role)
            .Custom((role, ctx) =>
            {
                bool isValidRole = roles.Contains(role);
                if (!isValidRole)
                {
                    ctx.AddFailure($"You must choose a role from: [{Client}, {Contributor}].");
                }
            });

        RuleFor(r => r.Username)
            .NotEmpty().WithMessage(RequiredError)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage(RequiredError)
            .Matches(Regexes.Email).WithMessage(EmailError);

        RuleFor(r => r.FirstName)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.LastName)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(RequiredError)
            .Length(PasswordMinLength, PasswordMaxLength).WithMessage(LengthError);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty().WithMessage(RequiredError)
            .Equal(r => r.Password).WithMessage("Passwords must be equal!");
    }
}
