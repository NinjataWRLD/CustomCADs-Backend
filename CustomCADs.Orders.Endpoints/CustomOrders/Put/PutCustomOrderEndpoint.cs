using CustomCADs.Orders.Application.CustomOrders.Commands.Edit;
using CustomCADs.Orders.Application.CustomOrders.Queries.IsBuyer;
using CustomCADs.Orders.Endpoints.Helpers;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Put;

using static ApiMessages;

public class PutCustomOrderEndpoint(IRequestSender sender)
    : Endpoint<PutCustomOrderRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<CustomOrdersGroup>();
        Description(d => d.WithSummary("4. I want to edit my Order"));
    }

    public override async Task HandleAsync(PutCustomOrderRequest req, CancellationToken ct)
    {
        CustomOrderId id = new(req.Id);
        IsCustomOrderBuyerQuery query = new(id, User.GetAccountId());
        bool isUserBuyer = await sender.SendQueryAsync(query, ct);

        if (!isUserBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        EditCustomOrderCommand command = new(id, req.Name, req.Description);
        await sender.SendCommandAsync(command, ct);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
