using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;

using static Constants.FluentMessages;

public class GetMaterialTexturePresignedUrlPostValidator : QueryValidator<GetMaterialTexturePresignedUrlPostQuery, UploadFileResponse>
{
	public GetMaterialTexturePresignedUrlPostValidator()
	{
		RuleFor(x => x.MaterialName)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Image.ContentType)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Image.FileName)
			.NotEmpty().WithMessage(RequiredError);
	}
}
