using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Gallery.Application.Carts.Queries.IsBuyer;

public class IsCartBuyerHandler(ICartReads reads)
    : IQueryHandler<IsCartBuyerQuery, bool>
{
    public async Task<bool> Handle(IsCartBuyerQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct)
            ?? throw CartNotFoundException.ById(req.Id);

        return cart.BuyerId == req.UserId;
    }
}
