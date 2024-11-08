using CustomCADs.Account.Domain.Roles;
using FluentValidation;

namespace CustomCADs.Account.Endpoints.Roles.PostRole;

using static Constants.Errors;
using static RoleConstants;

public class PostRoleRequestValidator : Validator<PostRoleRequest>
{
    public PostRoleRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthErrorMessage);
    }
}
