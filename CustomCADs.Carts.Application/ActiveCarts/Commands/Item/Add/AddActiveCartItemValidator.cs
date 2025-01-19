using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Add;

using static ActiveCartConstants.ActiveCartItems;
using static Constants.FluentMessages;

public class AddActiveCartItemValidator : CommandValidator<AddActiveCartItemCommand, ActiveCartItemId>
{
    public AddActiveCartItemValidator()
    {
        RuleFor(x => x.Weight)
            .InclusiveBetween(WeightMin, WeightMax).WithMessage(RangeError);
    }
}
