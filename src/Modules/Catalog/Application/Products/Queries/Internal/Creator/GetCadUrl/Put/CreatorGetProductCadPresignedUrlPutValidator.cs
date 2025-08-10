using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

using static Constants.FluentMessages;

public class CreatorGetProductCadPresignedUrlPutValidator : QueryValidator<CreatorGetProductCadPresignedUrlPutQuery, CreatorGetProductCadPresignedUrlPutDto>
{
	public CreatorGetProductCadPresignedUrlPutValidator()
	{
		RuleFor(x => x.NewCad.ContentType)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.NewCad.FileName)
			.NotEmpty().WithMessage(RequiredError);
	}
}
