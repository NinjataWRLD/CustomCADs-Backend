using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;

public sealed class DesignerGetCompletedOrderByIdHandler(ICompletedOrderReads reads, IRequestSender sender)
    : IQueryHandler<DesignerGetCompletedOrderByIdQuery, DesignerGetCompletedOrderByIdDto>
{
    public async Task<DesignerGetCompletedOrderByIdDto> Handle(DesignerGetCompletedOrderByIdQuery req, CancellationToken ct)
    {
        CompletedOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<CompletedOrder>.ById(req.Id);

        if (order.DesignerId != req.DesignerId)
            throw CustomAuthorizationException<CompletedOrder>.ById(order.Id);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        return order.ToDesignerGetOrderByIdDto(buyer);
    }
}
