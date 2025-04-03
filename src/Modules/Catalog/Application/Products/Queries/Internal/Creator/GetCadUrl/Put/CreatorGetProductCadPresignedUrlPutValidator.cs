using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

using static Constants.FluentMessages;

public class CreatorGetProductCadPresignedUrlPutValidator : QueryValidator<CreatorGetProductCadPresignedUrlPutQuery, CreatorGetProductCadPresignedUrlPutDto>
{
    public CreatorGetProductCadPresignedUrlPutValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(RequiredError);
    }
}
