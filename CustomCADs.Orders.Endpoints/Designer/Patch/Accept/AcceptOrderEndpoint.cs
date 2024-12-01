using CustomCADs.Orders.Application.Orders.Commands.Accept;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Accept;

public class AcceptOrderEndpoint(IRequestSender sender)
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
        OrderId id = new(req.Id);
        AcceptOrderCommand command = new(id, User.GetAccountId());
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
