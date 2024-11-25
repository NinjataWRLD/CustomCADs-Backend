using CustomCADs.Orders.Application.Orders.Commands.Edit;
using CustomCADs.Orders.Application.Orders.Queries.IsBuyer;
using CustomCADs.Orders.Endpoints.Helpers;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Endpoints.Client.Put;

using static ApiMessages;

public class PutOrderEndpoint(IRequestSender sender)
    : Endpoint<PutOrderRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ClientGroup>();
        Description(d => d.WithSummary("5. I want to edit my Order"));
    }

    public override async Task HandleAsync(PutOrderRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        IsOrderBuyerQuery query = new(id, User.GetAccountId());
        bool isUserBuyer = await sender.SendQueryAsync(query, ct);

        if (!isUserBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        EditOrderCommand command = new(id, req.Name, req.Description);
        await sender.SendCommandAsync(command, ct);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
