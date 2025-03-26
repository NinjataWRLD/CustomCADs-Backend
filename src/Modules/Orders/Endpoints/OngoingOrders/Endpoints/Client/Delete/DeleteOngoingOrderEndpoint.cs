using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Delete;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Delete;

public sealed class DeleteOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<DeleteOngoingOrderRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("08. Delete")
            .WithDescription("Delete your Order")
        );
    }

    public override async Task HandleAsync(DeleteOngoingOrderRequest req, CancellationToken ct)
    {
        DeleteOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
