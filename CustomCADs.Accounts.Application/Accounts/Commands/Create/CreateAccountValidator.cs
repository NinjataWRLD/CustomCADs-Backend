using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Create;

using static AccountConstants;
using static Constants;
using static Constants.FluentMessages;

public class CreateAccountValidator : Validator<CreateAccountCommand, AccountId>
{
    public CreateAccountValidator()
    {
        RuleFor(r => r.Role)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.Username)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(RequiredError)
            .Length(PasswordMinLength, PasswordMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage(RequiredError)
            .Matches(Regexes.Email).WithMessage(EmailError);

        RuleFor(r => r.TimeZone)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.FirstName)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.LastName)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);
    }
}
