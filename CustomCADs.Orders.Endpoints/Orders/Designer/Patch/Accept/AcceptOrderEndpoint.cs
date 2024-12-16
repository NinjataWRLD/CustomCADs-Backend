using CustomCADs.Orders.Application.Orders.Commands.Accept;

namespace CustomCADs.Orders.Endpoints.Orders.Designer.Patch.Accept;

public sealed class AcceptOrderEndpoint(IRequestSender sender)
    : Endpoint<AcceptOrderRequest>
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

    public override async Task HandleAsync(AcceptOrderRequest req, CancellationToken ct)
    {
        AcceptOrderCommand command = new(
            Id: new OrderId(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
