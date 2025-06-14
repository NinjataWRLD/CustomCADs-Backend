﻿using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Images.Commands.Shared.SetKey;

using static Constants.FluentMessages;

public class SetImageKeyValidator : CommandValidator<SetImageKeyCommand>
{
	public SetImageKeyValidator()
	{
		RuleFor(x => x.Key)
			.NotEmpty().WithMessage(RequiredError);
	}
}
