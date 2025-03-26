using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Accept;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Patch.Accept;

public sealed class AcceptOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<AcceptOngoingOrderRequest>
{
    public override void Configure()
    {
        Patch("accept");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("03. Accept")
            .WithDescription("Set an Order's Status to Accepted by specifying its Id")
        );
    }

    public override async Task HandleAsync(AcceptOngoingOrderRequest req, CancellationToken ct)
    {
        AcceptOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
