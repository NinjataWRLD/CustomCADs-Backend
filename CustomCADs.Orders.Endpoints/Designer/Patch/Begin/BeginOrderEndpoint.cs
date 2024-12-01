using CustomCADs.Orders.Application.Orders.Commands.Begin;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Begin;

public class BeginOrderEndpoint(IRequestSender sender)
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
        OrderId id = new(req.Id);
        BeginOrderCommand command = new(id, User.GetAccountId());
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
