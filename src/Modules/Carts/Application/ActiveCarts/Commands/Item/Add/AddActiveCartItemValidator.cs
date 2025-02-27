using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Add;

using static ActiveCartConstants.ActiveCartItems;
using static Constants.FluentMessages;

public class AddActiveCartItemValidator : CommandValidator<AddActiveCartItemCommand>
{
    public AddActiveCartItemValidator()
    {
        RuleFor(x => x.Weight)
            .InclusiveBetween(WeightMin, WeightMax).WithMessage(RangeError);
    }
}
