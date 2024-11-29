using CustomCADs.Account.Domain.Roles;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Account.Application.Roles.Commands.Create;

using static Constants.FluentMessages;
using static RoleConstants;

public class CreateRoleCommandValidator : Validator<CreateRoleCommand, RoleId>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Dto.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Dto.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
