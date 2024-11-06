using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Auth.Endpoints.SignUp.Register;

using static ApiMessages;
using static AuthConstants;
using static Constants;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        string[] roles = [Client, Contributor, Designer, Admin];

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
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .EmailAddress();

        RuleFor(r => r.FirstName)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.LastName)
            .Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(PasswordMinLength, PasswordMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Equal(r => r.Password).WithMessage("Passwords must be equal!");
    }
}
