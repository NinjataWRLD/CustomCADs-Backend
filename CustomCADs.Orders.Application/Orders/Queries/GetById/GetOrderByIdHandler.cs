using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public class GetOrderByIdHandler(IOrderReads reads)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdDto>
{
    public async Task<GetOrderByIdDto> Handle(GetOrderByIdQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.BuyerId == req.BuyerId)
        {
            throw OrderValidationException.Custom("Cannot modify another Buyer's Orders.");
        }

        return order.ToGetOrderByIdDto();
    }
}
