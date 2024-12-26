using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common.Exceptions.CartItems;

namespace CustomCADs.Carts.Application.Carts.Queries.GetItem;

public sealed class GetCartItemByIdHandler(ICartReads reads)
    : IQueryHandler<GetCartItemByIdQuery, CartItemDto>
{
    public async Task<CartItemDto> Handle(GetCartItemByIdQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        CartItem item = cart.Items.FirstOrDefault(i => i.Id == req.ItemId)
            ?? throw CartItemNotFoundException.ById(req.ItemId);

        return item.ToCartItemDto();
    }
}
