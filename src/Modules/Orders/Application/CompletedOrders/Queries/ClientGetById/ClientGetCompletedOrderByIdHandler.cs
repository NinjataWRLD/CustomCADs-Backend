using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
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

        GetTimeZoneByIdQuery timeZoneQuery = new(order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        GetUsernameByIdQuery designerQuery = new(order.BuyerId);
        string designer = await sender.SendQueryAsync(designerQuery, ct).ConfigureAwait(false);

        return order.ToGetOrderByIdDto(timeZone, designer);
    }
}
