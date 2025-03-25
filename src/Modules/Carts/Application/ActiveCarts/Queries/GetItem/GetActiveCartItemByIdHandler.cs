using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.GetItem;

public sealed class GetActiveCartItemByIdHandler(IActiveCartReads reads)
    : IQueryHandler<GetActiveCartItemByIdQuery, ActiveCartItemDto>
{
    public async Task<ActiveCartItemDto> Handle(GetActiveCartItemByIdQuery req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCart>.ById(req.BuyerId);

        ActiveCartItem item = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId)
            ?? throw CustomNotFoundException<ActiveCartItem>.ById(req.ProductId);

        return item.ToCartItemDto();
    }
}
