using CustomCADs.Orders.Application.Orders.Commands.Finish;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Finish;

public class FinishOrderEndpoint(IRequestSender sender)
    : Endpoint<FinishOrderRequest>
{
    public override void Configure()
    {
        Patch("finish");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to Finish an Order"));
    }

    public override async Task HandleAsync(FinishOrderRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);

        (string Key, string ContentType)? cad = null;
        if (req.CadKey is not null && req.CadContentType is not null)
            cad = (req.CadKey, req.CadContentType);

        FinishOrderCommand command = new(id, User.GetAccountId(), cad);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
