using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;

using static Constants.FluentMessages;

public class GetMaterialTexturePresignedUrlPostValidator : QueryValidator<GetMaterialTexturePresignedUrlPostQuery, GetMaterialTexturePresignedUrlPostDto>
{
    public GetMaterialTexturePresignedUrlPostValidator()
    {
        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
