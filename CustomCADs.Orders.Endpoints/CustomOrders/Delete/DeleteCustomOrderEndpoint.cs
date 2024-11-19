using CustomCADs.Orders.Application.CustomOrders.Commands.Delete;
using CustomCADs.Orders.Application.CustomOrders.Queries.IsBuyer;
using CustomCADs.Orders.Endpoints.Helpers;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Delete;

using static ApiMessages;

public class DeleteCustomOrderEndpoint(IRequestSender sender)
    : Endpoint<DeleteCustomOrderRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<CustomOrdersGroup>();
        Description(d => d.WithSummary("6. I want to delete my Order"));
    }

    public override async Task HandleAsync(DeleteCustomOrderRequest req, CancellationToken ct)
    {
        CustomOrderId id = new(req.Id);
        IsCustomOrderBuyerQuery isBuyerQuery = new(id, User.GetAccountId());

        bool isUserBuyer = await sender.SendQueryAsync(isBuyerQuery, ct).ConfigureAwait(false);
        if (!isUserBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        DeleteCustomOrderCommand command = new(id);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
