using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetItem;

public sealed class GetPurchasedCartItemByIdHandler(IPurchasedCartReads reads)
    : IQueryHandler<GetPurchasedCartItemByIdQuery, PurchasedCartItemDto>
{
    public async Task<PurchasedCartItemDto> Handle(GetPurchasedCartItemByIdQuery req, CancellationToken ct)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<PurchasedCart>.ById(req.Id);

        PurchasedCartItem item = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId)
            ?? throw CustomNotFoundException<PurchasedCartItem>.ById(req.ProductId);

        return item.ToDto();
    }
}
