using CustomCADs.Account.Domain.Users.Validation;
using FluentValidation;

namespace CustomCADs.Account.Endpoints.Users.Post;

using static Constants.FluentMessages;
using static UserConstants;

public class PostUserRequestValidator : Validator<PostUserRequest>
{
    public PostUserRequestValidator()
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
