using CustomCADs.Orders.Application.OngoingOrders.Commands.Edit;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Put;

public sealed class PutOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<PutOngoingOrderRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("06. Edit")
            .WithDescription("Edit your Order by specifying its Id and providing a new Name and Description")
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
