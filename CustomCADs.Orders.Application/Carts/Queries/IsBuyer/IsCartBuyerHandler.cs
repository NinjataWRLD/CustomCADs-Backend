using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Carts.Reads;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Orders.Application.Carts.Queries.IsBuyer;

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
