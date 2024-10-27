using FastEndpoints;
using FluentValidation;
using static CustomCADs.Account.Domain.Roles.RoleConstants;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Account.Endpoints.Roles.PostRole;

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
