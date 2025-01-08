using CustomCADs.Orders.Application.Orders.Commands.Cancel;

namespace CustomCADs.Orders.Endpoints.Orders.Designer.Patch.Cancel;

public sealed class CancelOrderEndpoint(IRequestSender sender)
    : Endpoint<CancelOrderRequest>
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

    public override async Task HandleAsync(CancelOrderRequest req, CancellationToken ct)
    {
        CancelOrderCommand command = new(
            Id: OrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
