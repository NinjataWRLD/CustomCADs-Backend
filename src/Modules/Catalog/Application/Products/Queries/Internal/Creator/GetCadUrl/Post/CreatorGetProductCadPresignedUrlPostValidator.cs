using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using CustomCADs.Shared.Application.Dtos.Files;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

using static Constants.FluentMessages;

public class CreatorGetProductCadPresignedUrlPostValidator : QueryValidator<CreatorGetProductCadPresignedUrlPostQuery, UploadFileResponse>
{
	public CreatorGetProductCadPresignedUrlPostValidator()
	{
		RuleFor(x => x.ProductName)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Cad.ContentType)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Cad.FileName)
			.NotEmpty().WithMessage(RequiredError);
	}
}
