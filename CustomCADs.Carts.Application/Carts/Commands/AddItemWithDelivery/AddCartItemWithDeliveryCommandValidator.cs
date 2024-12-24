using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItemWithDelivery;

using static CartConstants.CartItems;
using static Constants.FluentMessages;

public class AddCartItemWithDeliveryCommandValidator : Validator<AddCartItemWithDeliveryCommand, CartItemId>
{
    public AddCartItemWithDeliveryCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .ExclusiveBetween(QuantityMin, QuantityMax).WithMessage(RangeError);

        RuleFor(i => i.Weight)
            .ExclusiveBetween(WeightMin, WeightMax).WithMessage(RangeError);
    }
}
