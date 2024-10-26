using FastEndpoints;
using FluentValidation;
using static CustomCADs.Auth.Infrastructure.AuthConstants;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Auth.Endpoints.Auth.Register;

using static Helpers.ApiMessages;

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
                    string error = string.Format(ForbiddenRoleRegister, roles);
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
