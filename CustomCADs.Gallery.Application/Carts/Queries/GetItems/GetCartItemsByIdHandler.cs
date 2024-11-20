using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Reads;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public class GetCartItemsByIdHandler(ICartReads reads)
    : IQueryHandler<GetCartItemsByIdCommand, ICollection<GetCartItemsByIdDto>>
{
    public async Task<ICollection<GetCartItemsByIdDto>> Handle(GetCartItemsByIdCommand req, CancellationToken ct)
    {
        ICollection<CartItem> items = await reads.ItemsByIdAsync(req.Id, ct: ct);

        return items.Select(o => o.ToGetCartItemsByIdDto()).ToArray();
    }
}
