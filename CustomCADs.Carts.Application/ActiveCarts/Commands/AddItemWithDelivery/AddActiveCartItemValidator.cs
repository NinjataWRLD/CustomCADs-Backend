using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.AddItemWithDelivery;

using static ActiveCartConstants.ActiveCartItems;
using static Constants.FluentMessages;

public class AddActiveCartItemWithDeliveryValidator : Validator<AddActiveCartItemWithDeliveryCommand, ActiveCartItemId>
{
    public AddActiveCartItemWithDeliveryValidator()
    {
        RuleFor(x => x.Weight)
            .InclusiveBetween(WeightMin, WeightMax).WithMessage(RangeError);
    }
}
