using CustomCADs.Orders.Application.Carts.Commands.RemoveOrder;
using CustomCADs.Orders.Application.Carts.Queries.IsBuyer;
using CustomCADs.Orders.Endpoints.Helpers;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Carts.RemoveOrder;

using static ApiMessages;

public class RemoveCartOrderEndpoint(IRequestSender sender)
    : Endpoint<RemoveCartOrderRequest>
{
    public override void Configure()
    {
        Put("removeOrder");
        Group<CartsGroup>();
        Description(d => d.WithSummary("4. I want to remove an Order from my Cart."));
    }

    public override async Task HandleAsync(RemoveCartOrderRequest req, CancellationToken ct)
    {
        CartId id = new(req.CartId);
        IsCartBuyerQuery query = new(id, User.GetAccountId());

        bool userIsBuyer = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        if (!userIsBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        RemoveCartOrderCommand commnad = new(
            Id: id,
            OrderId: new(req.OrderId)
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
