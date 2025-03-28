using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

using static Constants.FluentMessages;

public class CreatorGetProductCadPresignedUrlPostValidator : QueryValidator<CreatorGetProductCadPresignedUrlPostQuery, CreatorGetProductCadPresignedUrlPostDto>
{
    public CreatorGetProductCadPresignedUrlPostValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
