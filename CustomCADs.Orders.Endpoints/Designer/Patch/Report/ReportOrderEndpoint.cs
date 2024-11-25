using CustomCADs.Orders.Application.Orders.Commands.Report;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Report;

public class ReportOrderEndpoint(IRequestSender sender)
    : Endpoint<ReportOrderRequest>
{
    public override void Configure()
    {
        Patch("report");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to Report the Order"));
    }

    public override async Task HandleAsync(ReportOrderRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        ReportOrderCommand command = new(id, User.GetAccountId());
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
