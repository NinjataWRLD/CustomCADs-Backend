using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Accounts.Application.Roles.Commands.Edit;

using static Constants.FluentMessages;
using static RoleConstants;

public class EditRoleValidator : CommandValidator<EditRoleCommand>
{
    public EditRoleValidator()
    {
        RuleFor(r => r.Dto.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Dto.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
