using FastEndpoints;
using FluentValidation;
using static CustomCADs.Account.Domain.Users.UserConstants;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Account.Endpoints.Users.PostUser;

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
