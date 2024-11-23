using CustomCADs.Orders.Application.Orders.Commands.Begin;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Begin;

public class BeginOrderEndpoint(IRequestSender sender)
    : Endpoint<BeginOrderRequest>
{
    public override void Configure()
    {
        Patch("begin");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to Beign the Order"));
    }

    public override async Task HandleAsync(BeginOrderRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        BeginOrderCommand command = new(id, User.GetAccountId());
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
