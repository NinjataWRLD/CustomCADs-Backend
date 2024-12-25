using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Orders.Application.Orders.Commands.Edit;

using static Constants.FluentMessages;
using static OrderConstants;

public class EditOrderValidator : Validator<EditOrderCommand>
{
    public EditOrderValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
