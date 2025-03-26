using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Edit;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Put;

public sealed class PutOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<PutOngoingOrderRequest>
{
    public override void Configure()
    {
        Put("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("06. Edit")
            .WithDescription("Edit your Order")
        );
    }

    public override async Task HandleAsync(PutOngoingOrderRequest req, CancellationToken ct)
    {
        EditOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            Name: req.Name,
            Description: req.Description,
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
