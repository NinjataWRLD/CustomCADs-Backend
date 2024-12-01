using CustomCADs.Gallery.Application.Carts.Exceptions;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public class GetCartItemsByIdHandler(ICartReads reads)
    : IQueryHandler<GetCartItemsByIdQuery, ICollection<GetCartItemsByIdDto>>
{
    public async Task<ICollection<GetCartItemsByIdDto>> Handle(GetCartItemsByIdQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        return [.. cart.Items.Select(o => o.ToGetCartItemsByIdDto())];
    }
}
