using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Auth.Endpoints.SignUp.Register;

using static ApiMessages;
using static AuthConstants;
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
                    string error = string.Format(ForbiddenRoleRegister,
                        string.Join(", ", [Client, Contributor]));
                    ctx.AddFailure(error);
                }
            });

        RuleFor(r => r.Username)
            .NotEmpty().WithMessage(RequiredError)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage(RequiredError)
            .EmailAddress();

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
