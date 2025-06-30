using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

using static Constants.FluentMessages;

public class GetCustomCadPresignedUrlPostValidator : QueryValidator<GetCustomCadPresignedUrlPostQuery, UploadFileResponse>
{
	public GetCustomCadPresignedUrlPostValidator()
	{
		RuleFor(x => x.Cad.ContentType)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Cad.FileName)
			.NotEmpty().WithMessage(RequiredError);
	}
}
