using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Finish;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Patch.Finish;

public sealed class FinishOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<FinishOngoingOrderRequest>
{
    public override void Configure()
    {
        Patch("finish");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Finish")
            .WithDescription("Set an Order's Status to Finished by specifying its Id and Cad")
        );
    }

    public override async Task HandleAsync(FinishOngoingOrderRequest req, CancellationToken ct)
    {
        FinishOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            Price: req.Price,
            Cad: req.ToTuple(),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
