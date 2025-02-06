using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetImageUrl.Put;

using static Constants.FluentMessages;

public class GetProductImagePresignedUrlPutValidator : QueryValidator<GetProductImagePresignedUrlPutQuery, GetProductImagePresignedUrlPutDto>
{
    public GetProductImagePresignedUrlPutValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
