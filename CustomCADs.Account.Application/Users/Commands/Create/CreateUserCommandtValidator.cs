using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Account.Application.Users.Commands.Create;

using static Constants.FluentMessages;
using static UserConstants;

public class CreateUserCommandtValidator : Validator<CreateUserCommand, UserId>
{
    public CreateUserCommandtValidator()
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
