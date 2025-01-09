using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;

public sealed class ClientGetCompletedOrderByIdHandler(ICompletedOrderReads reads, IRequestSender sender)
    : IQueryHandler<ClientGetCompletedOrderByIdQuery, ClientGetCompletedOrderByIdDto>
{
    public async Task<ClientGetCompletedOrderByIdDto> Handle(ClientGetCompletedOrderByIdQuery req, CancellationToken ct)
    {
        CompletedOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CompletedOrderNotFoundException.ById(req.Id);

        if (order.BuyerId != req.BuyerId)
        {
            throw CompletedOrderAuthorizationException.ByOrderId(req.Id);
        }

        GetTimeZoneByIdQuery timeZoneQuery = new(Id: order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return order.ToGetOrderByIdDto(timeZone);
    }
}
