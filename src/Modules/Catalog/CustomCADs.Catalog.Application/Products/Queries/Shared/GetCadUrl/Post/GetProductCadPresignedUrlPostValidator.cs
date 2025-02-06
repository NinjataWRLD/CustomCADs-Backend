using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post;

using static Constants.FluentMessages;

public class GetProductCadPresignedUrlPostValidator : QueryValidator<GetProductCadPresignedUrlPostQuery, GetProductCadPresignedUrlPostDto>
{
    public GetProductCadPresignedUrlPostValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
