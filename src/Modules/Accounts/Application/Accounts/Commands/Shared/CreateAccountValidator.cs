﻿using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.UseCases.Accounts.Commands;
using FluentValidation;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Shared;

using static AccountConstants;
using static Constants;
using static Constants.FluentMessages;

public class CreateAccountValidator : CommandValidator<CreateAccountCommand, AccountId>
{
	public CreateAccountValidator()
	{
		RuleFor(r => r.Role)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(r => r.Username)
			.NotEmpty().WithMessage(RequiredError)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.Email)
			.NotEmpty().WithMessage(RequiredError)
			.Matches(Regexes.Email).WithMessage(EmailError);

		RuleFor(r => r.FirstName)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.LastName)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);
	}
}
