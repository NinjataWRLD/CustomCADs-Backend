using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;

using static Constants.FluentMessages;
using static CustomConstants;

public class CreateCustomValidator : CommandValidator<CreateCustomCommand, CustomId>
{
    public CreateCustomValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
