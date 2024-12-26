using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public sealed class GetOrderByIdHandler(IOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdDto>
{
    public async Task<GetOrderByIdDto> Handle(GetOrderByIdQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
        {
            throw OrderAuthorizationException.ByOrderId(req.Id);
        }

        GetTimeZoneByIdQuery timeZoneQuery = new(Id: order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return order.ToGetOrderByIdDto(timeZone);
    }
}
