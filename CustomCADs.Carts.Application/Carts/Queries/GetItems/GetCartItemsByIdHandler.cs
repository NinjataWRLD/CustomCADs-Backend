using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts.Reads;

namespace CustomCADs.Carts.Application.Carts.Queries.GetItems;

public sealed class GetCartItemsByIdHandler(ICartReads reads)
    : IQueryHandler<GetCartItemsByIdQuery, ICollection<CartItemDto>>
{
    public async Task<ICollection<CartItemDto>> Handle(GetCartItemsByIdQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        return [.. cart.Items.Select(o => o.ToCartItemDto())];
    }
}
