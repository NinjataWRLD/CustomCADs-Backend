using CustomCADs.Catalog.Domain.Tags;
using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;

using static Constants.FluentMessages;
using static TagConstants;

public class CreateTagValidator : CommandValidator<CreateTagCommand, TagId>
{
    public CreateTagValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);
    }
}
