using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.AddItem;

using static ActiveCartConstants.ActiveCartItems;
using static Constants.FluentMessages;

public class AddActiveCartItemValidator : Validator<AddActiveCartItemCommand, ActiveCartItemId>
{
    public AddActiveCartItemValidator()
    {
        RuleFor(x => x.Weight)
            .InclusiveBetween(WeightMin, WeightMax).WithMessage(RangeError);
    }
}
