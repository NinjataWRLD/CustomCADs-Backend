using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Put;

using static Constants.FluentMessages;

public class GetMaterialTexturePresignedUrlPutValidator : QueryValidator<GetMaterialTexturePresignedUrlPutQuery, GetMaterialTexturePresignedUrlPutDto>
{
	public GetMaterialTexturePresignedUrlPutValidator()
	{
		RuleFor(x => x.NewImage.ContentType)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.NewImage.FileName)
			.NotEmpty().WithMessage(RequiredError);
	}
}
