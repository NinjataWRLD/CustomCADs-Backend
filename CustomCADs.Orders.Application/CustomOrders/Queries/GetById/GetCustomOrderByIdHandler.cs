using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.GetById;

public class GetCustomOrderByIdHandler(ICustomOrderReads reads)
    : IQueryHandler<GetCustomOrderByIdQuery, GetCustomOrderByIdDto>
{
    public async Task<GetCustomOrderByIdDto> Handle(GetCustomOrderByIdQuery req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        return order.ToGetCustomOrderByIdDto();
    }
}
