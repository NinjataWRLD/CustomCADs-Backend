using CustomCADs.Orders.Application.Orders.Commands.Begin;

namespace CustomCADs.Orders.Endpoints.Orders.Designer.Patch.Begin;

public sealed class BeginOrderEndpoint(IRequestSender sender)
    : Endpoint<BeginOrderRequest>
{
    public override void Configure()
    {
        Patch("begin");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("05. Begin")
            .WithDescription("Set an Order's Status to Begun by specifying its Id")
        );
    }

    public override async Task HandleAsync(BeginOrderRequest req, CancellationToken ct)
    {
        BeginOrderCommand command = new(
            Id: OrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
