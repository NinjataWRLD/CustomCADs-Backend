using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItemWithDelivery;

using static CartConstants.CartItems;
using static Constants.FluentMessages;

public class AddCartItemWithDeliveryValidator : Validator<AddCartItemWithDeliveryCommand, CartItemId>
{
    public AddCartItemWithDeliveryValidator()
    {
        RuleFor(x => x.Weight)
            .InclusiveBetween(WeightMin, WeightMax).WithMessage(RangeError);

        RuleFor(x => x.Quantity)
            .InclusiveBetween(QuantityMin, QuantityMax).WithMessage(RangeError);
    }
}
