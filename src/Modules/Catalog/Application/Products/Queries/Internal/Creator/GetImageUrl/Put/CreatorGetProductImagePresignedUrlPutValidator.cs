using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

using static Constants.FluentMessages;

public class CreatorGetProductImagePresignedUrlPutValidator : QueryValidator<CreatorGetProductImagePresignedUrlPutQuery, CreatorGetProductImagePresignedUrlPutDto>
{
    public CreatorGetProductImagePresignedUrlPutValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
