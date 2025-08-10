using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using CustomCADs.Shared.Application.Dtos.Files;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

using static Constants.FluentMessages;

public class CreatorGetProductImagePresignedUrlPostValidator : QueryValidator<CreatorGetProductImagePresignedUrlPostQuery, UploadFileResponse>
{
	public CreatorGetProductImagePresignedUrlPostValidator()
	{
		RuleFor(x => x.ProductName)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Image.ContentType)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Image.FileName)
			.NotEmpty().WithMessage(RequiredError);
	}
}
