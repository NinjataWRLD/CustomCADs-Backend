using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.GetAll;

public class GetAllCustomOrdersHandler(ICustomOrderReads reads) 
    : IQueryHandler<GetAllCustomOrdersQuery, GetAllCustomOrdersDto>
{
    public async Task<GetAllCustomOrdersDto> Handle(GetAllCustomOrdersQuery req, CancellationToken ct)
    {
        CustomOrderQuery query = new(
            DeliveryType: req.DeliveryType,
            OrderStatus: req.OrderStatus,
            BuyerId: req.BuyerId,
            DesignerId: req.DesignerId,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        CustomOrderResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return new(
            result.Count, 
            result.Orders
                .Select(o => o.ToGetAllCustomOrdersItem())
                .ToArray()
        );
    }
}
