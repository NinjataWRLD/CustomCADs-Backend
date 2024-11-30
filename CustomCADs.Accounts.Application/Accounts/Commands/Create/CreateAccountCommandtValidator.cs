using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Create;

using static AccountConstants;
using static Constants.FluentMessages;

public class CreateAccountCommandtValidator : Validator<CreateAccountCommand, AccountId>
{
    public CreateAccountCommandtValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.Role)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.FirstName)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.LastName)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);
    }
}
