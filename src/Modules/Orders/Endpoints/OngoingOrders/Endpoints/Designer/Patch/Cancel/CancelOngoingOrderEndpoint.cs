using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Cancel;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Patch.Cancel;

public sealed class CancelOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<CancelOngoingOrderRequest>
{
    public override void Configure()
    {
        Patch("cancel");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("07. Cancel")
            .WithDescription("Set an Order's Status to back Pending by specifying its Id")
        );
    }

    public override async Task HandleAsync(CancelOngoingOrderRequest req, CancellationToken ct)
    {
        CancelOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
