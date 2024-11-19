using CustomCADs.Gallery.Domain.Carts;
using FluentValidation;

namespace CustomCADs.Gallery.Endpoints.Carts.AddItem;

using static CartConstants.CartItems;
using static Constants.FluentMessages;

public class AddCartItemRequestValidator : Validator<AddCartItemRequest>
{
    public AddCartItemRequestValidator()
    {
        RuleFor(i => i.Quantity)
            .ExclusiveBetween(QuantityMin, QuantityMax).WithMessage(RangeError);
    }
}
