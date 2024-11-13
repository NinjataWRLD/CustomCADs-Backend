using CustomCADs.Account.Domain.Roles.Validation;
using FluentValidation;

namespace CustomCADs.Account.Endpoints.Roles.PostRole;

using static Constants.FluentMessages;
using static RoleConstants;

public class PostRoleRequestValidator : Validator<PostRoleRequest>
{
    public PostRoleRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
