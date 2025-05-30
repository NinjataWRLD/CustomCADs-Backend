using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetKey;

using static Constants.FluentMessages;

public class SetCadKeyValidator : CommandValidator<SetCadKeyCommand>
{
	public SetCadKeyValidator()
	{
		RuleFor(x => x.Key)
			.NotEmpty().WithMessage(RequiredError);
	}
}
