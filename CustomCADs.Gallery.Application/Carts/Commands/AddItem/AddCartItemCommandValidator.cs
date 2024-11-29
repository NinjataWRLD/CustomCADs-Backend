using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Gallery.Application.Carts.Commands.AddItem;

using static CartConstants.CartItems;
using static Constants.FluentMessages;

public class AddCartItemCommandValidator : Validator<AddCartItemCommand, CartItemId>
{
    public AddCartItemCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .ExclusiveBetween(QuantityMin, QuantityMax).WithMessage(RangeError);
    }
}
