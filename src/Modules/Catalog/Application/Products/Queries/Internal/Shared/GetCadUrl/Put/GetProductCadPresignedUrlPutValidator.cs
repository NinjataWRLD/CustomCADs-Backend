using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Put;

using static Constants.FluentMessages;

public class GetProductCadPresignedUrlPutValidator : QueryValidator<GetProductCadPresignedUrlPutQuery, GetProductCadPresignedUrlPutDto>
{
    public GetProductCadPresignedUrlPutValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
