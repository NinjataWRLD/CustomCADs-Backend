using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Carts.Reads;

namespace CustomCADs.Orders.Application.Carts.Queries.GetOrders;

public class GetCartOrdersByIdHandler(ICartReads reads)
    : IQueryHandler<GetCartOrdersByIdCommand, ICollection<GetCartOrdersByIdDto>>
{
    public async Task<ICollection<GetCartOrdersByIdDto>> Handle(GetCartOrdersByIdCommand req, CancellationToken ct)
    {
        ICollection<GalleryOrder> orders = await reads.OrdersByIdAsync(req.Id, ct: ct);

        return orders.Select(o => o.ToGetCartOrdersByIdDto()).ToArray();
    }
}
