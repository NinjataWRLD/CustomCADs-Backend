using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;

using static Constants.FluentMessages;
using static TagConstants;

public class EditTagValidator : CommandValidator<EditTagCommand>
{
	public EditTagValidator()
	{
		RuleFor(x => x.Name)
			.NotNull().WithMessage(RequiredError)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);
	}
}
