using CustomCADs.Orders.Application.Orders.Commands.Finish;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
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
        CadId? cadId = req.CadId is null ? null : new(req.CadId.Value);

        FinishOrderCommand command = new(id, User.GetAccountId(), cadId);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
