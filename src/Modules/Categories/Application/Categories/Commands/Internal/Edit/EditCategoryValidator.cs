using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Categories.Application.Categories.Commands.Internal.Edit;

using static CategoryConstants;
using static Constants.FluentMessages;

public class EditCategoryValidator : CommandValidator<EditCategoryCommand>
{
	public EditCategoryValidator()
	{
		RuleFor(x => x.Dto.Name)
			.NotEmpty().WithMessage(RequiredError)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

		RuleFor(x => x.Dto.Description)
			.NotEmpty().WithMessage(RequiredError)
			.Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
	}
}
