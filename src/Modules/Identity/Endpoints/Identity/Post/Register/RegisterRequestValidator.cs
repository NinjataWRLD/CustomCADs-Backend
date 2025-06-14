using CustomCADs.Identity.Domain.Users;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Register;

using static Constants;
using static Constants.FluentMessages;
using static UserConstants;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(r => r.Role)
			.Must(r => r is Roles.Customer or Roles.Contributor);

		RuleFor(r => r.Username)
			.NotEmpty().WithMessage(RequiredError)
			.Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.Email)
			.NotEmpty().WithMessage(RequiredError)
			.Matches(Regexes.Email).WithMessage(EmailError);

		RuleFor(r => r.FirstName)
			.Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.LastName)
			.Length(UsernameMinLength, UsernameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.Password)
			.NotEmpty().WithMessage(RequiredError)
			.MinimumLength(PasswordMinLength).WithMessage(LengthError);

		RuleFor(r => r.ConfirmPassword)
			.NotEmpty().WithMessage(RequiredError)
			.Equal(r => r.Password).WithMessage("Passwords must be equal!");
	}
}
