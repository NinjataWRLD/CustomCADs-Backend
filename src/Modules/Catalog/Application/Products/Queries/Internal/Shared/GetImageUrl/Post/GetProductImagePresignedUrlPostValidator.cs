using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post;

using static Constants.FluentMessages;

public class GetProductImagePresignedUrlPostValidator : QueryValidator<GetProductImagePresignedUrlPostQuery, GetProductImagePresignedUrlPostDto>
{
    public GetProductImagePresignedUrlPostValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
