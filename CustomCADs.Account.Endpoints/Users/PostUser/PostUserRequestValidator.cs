using CustomCADs.Account.Domain.Users;
using FluentValidation;

namespace CustomCADs.Account.Endpoints.Users.PostUser;

using static Constants;
using static UserConstants;

public class PostUserRequestValidator : Validator<PostUserRequest>
{
    public PostUserRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage(RequiredErrorMessage);

        RuleFor(r => r.Role)
            .NotEmpty().WithMessage(RequiredErrorMessage);

        RuleFor(r => r.FirstName)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.LastName)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthErrorMessage);
    }
}
