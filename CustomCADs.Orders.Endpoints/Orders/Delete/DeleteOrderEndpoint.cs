using CustomCADs.Orders.Application.Orders.Commands.Delete;
using CustomCADs.Orders.Application.Orders.Queries.IsBuyer;
using CustomCADs.Orders.Endpoints.Helpers;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Orders.Delete;

using static ApiMessages;

public class DeleteOrderEndpoint(IRequestSender sender)
    : Endpoint<DeleteOrderRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("7. I want to delete my Order"));
    }

    public override async Task HandleAsync(DeleteOrderRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        IsOrderBuyerQuery isBuyerQuery = new(id, User.GetAccountId());

        bool isUserBuyer = await sender.SendQueryAsync(isBuyerQuery, ct).ConfigureAwait(false);
        if (!isUserBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        DeleteOrderCommand command = new(id);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
