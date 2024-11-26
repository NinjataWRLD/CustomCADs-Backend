using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Reads;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public class GetCartItemsByIdHandler(ICartReads reads)
    : IQueryHandler<GetCartItemsByIdQuery, ICollection<GetCartItemsByIdDto>>
{
    public async Task<ICollection<GetCartItemsByIdDto>> Handle(GetCartItemsByIdQuery req, CancellationToken ct)
    {
        bool userOwnsCart = await reads
            .OwnsByIdAsync(req.Id, req.BuyerId, ct: ct)
            .ConfigureAwait(false);

        ICollection<CartItem> items = await reads
            .ItemsByIdAsync(req.Id, ct: ct)
            .ConfigureAwait(false);

        return [.. items.Select(o => o.ToGetCartItemsByIdDto())];
    }
}
