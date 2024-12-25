using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItem;

using static CartConstants.CartItems;
using static Constants.FluentMessages;

public class AddCartItemValidator : Validator<AddCartItemCommand, CartItemId>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.Weight)
            .InclusiveBetween(WeightMin, WeightMax).WithMessage(RangeError);
    }
}
