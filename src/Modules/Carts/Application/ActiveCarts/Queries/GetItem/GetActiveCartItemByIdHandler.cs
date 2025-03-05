using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.GetItem;

public sealed class GetActiveCartItemByIdHandler(IActiveCartReads reads)
    : IQueryHandler<GetActiveCartItemByIdQuery, ActiveCartItemDto>
{
    public async Task<ActiveCartItemDto> Handle(GetActiveCartItemByIdQuery req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        ActiveCartItem item = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId)
            ?? throw ActiveCartItemNotFoundException.ByProductId(req.ProductId);

        return item.ToCartItemDto();
    }
}
