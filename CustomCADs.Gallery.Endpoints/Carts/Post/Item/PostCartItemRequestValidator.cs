using CustomCADs.Gallery.Domain.Carts;
using FluentValidation;

namespace CustomCADs.Gallery.Endpoints.Carts.Post.Item;

using static CartConstants.CartItems;
using static Constants.FluentMessages;

public class PostCartItemRequestValidator : Validator<PostCartItemRequest>
{
    public PostCartItemRequestValidator()
    {
        RuleFor(i => i.Quantity)
            .ExclusiveBetween(QuantityMin, QuantityMax).WithMessage(RangeError);
    }
}
