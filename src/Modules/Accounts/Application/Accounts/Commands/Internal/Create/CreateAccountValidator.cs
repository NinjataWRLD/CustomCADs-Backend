﻿using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Internal.Create;

using static AccountConstants;
using static Constants;
using static Constants.FluentMessages;

public class CreateAccountValidator : CommandValidator<CreateAccountCommand, AccountId>
{
	public CreateAccountValidator()
	{
		RuleFor(r => r.Role)
			.NotEmpty().WithMessage(RequiredError)
			.Must(r => r is Roles.Customer or Roles.Contributor or Roles.Designer or Roles.Admin);

		RuleFor(r => r.Username)
			.NotEmpty().WithMessage(RequiredError)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.Password)
			.NotEmpty().WithMessage(RequiredError)
			.MinimumLength(PasswordMinLength).WithMessage(MinimumError);

		RuleFor(r => r.Email)
			.NotEmpty().WithMessage(RequiredError)
			.Matches(Regexes.Email).WithMessage(EmailError);

		RuleFor(r => r.FirstName)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.LastName)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);
	}
}
