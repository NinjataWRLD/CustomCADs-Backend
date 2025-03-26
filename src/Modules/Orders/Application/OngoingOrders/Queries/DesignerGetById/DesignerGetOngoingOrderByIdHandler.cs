using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;

public sealed class DesignerGetOngoingOrderByIdHandler(IOngoingOrderReads reads, IRequestSender sender)
    : IQueryHandler<DesignerGetOngoingOrderByIdQuery, DesignerGetOngoingOrderByIdDto>
{
    public async Task<DesignerGetOngoingOrderByIdDto> Handle(DesignerGetOngoingOrderByIdQuery req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (order.OrderStatus is not OngoingOrderStatus.Pending
            && order.DesignerId != req.DesignerId)
            throw CustomAuthorizationException<OngoingOrder>.ById(req.Id);

        GetUsernameByIdQuery buyerQuery = new(order.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        return order.ToDesignerGetByIdDto(buyer);
    }
}
