using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Put;

using static Constants.FluentMessages;

public class GetMaterialTexturePresignedUrlPutValidator : QueryValidator<GetMaterialTexturePresignedUrlPutQuery, GetMaterialTexturePresignedUrlPutDto>
{
    public GetMaterialTexturePresignedUrlPutValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
