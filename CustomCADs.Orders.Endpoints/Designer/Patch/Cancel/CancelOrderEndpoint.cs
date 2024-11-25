using CustomCADs.Orders.Application.Orders.Commands.Cancel;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Cancel;

public class CancelOrderEndpoint(IRequestSender sender)
    : Endpoint<CancelOrderRequest>
{
    public override void Configure()
    {
        Patch("cancel");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to Cancel the Order"));
    }

    public override async Task HandleAsync(CancelOrderRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        CancelOrderCommand command = new(id, User.GetAccountId());
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
