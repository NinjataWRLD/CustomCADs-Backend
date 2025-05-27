using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;

using static Constants.FluentMessages;
using static ProductConstants;

public class EditProductValidator : CommandValidator<EditProductCommand>
{
	public EditProductValidator()
	{
		RuleFor(r => r.Name)
			.NotEmpty().WithMessage(RequiredError)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.Description)
			.NotEmpty().WithMessage(RequiredError)
			.Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);

		RuleFor(r => r.Price)
			.ExclusiveBetween(PriceMin, PriceMax).WithMessage(RangeError);
	}
}
