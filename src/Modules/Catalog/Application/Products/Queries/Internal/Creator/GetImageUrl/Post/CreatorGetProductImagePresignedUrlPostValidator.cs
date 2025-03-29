using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

using static Constants.FluentMessages;

public class CreatorGetProductImagePresignedUrlPostValidator : QueryValidator<CreatorGetProductImagePresignedUrlPostQuery, CreatorGetProductImagePresignedUrlPostDto>
{
    public CreatorGetProductImagePresignedUrlPostValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
