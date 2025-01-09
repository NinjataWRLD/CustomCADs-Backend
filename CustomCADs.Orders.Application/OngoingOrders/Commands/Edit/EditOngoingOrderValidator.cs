using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Edit;

using static Constants.FluentMessages;
using static OngoingOrderConstants;

public class EditOngoingOrderValidator : Validator<EditOngoingOrderCommand>
{
    public EditOngoingOrderValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
