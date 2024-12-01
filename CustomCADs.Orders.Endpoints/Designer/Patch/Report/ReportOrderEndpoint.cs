using CustomCADs.Orders.Application.Orders.Commands.Report;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Report;

public class ReportOrderEndpoint(IRequestSender sender)
    : Endpoint<ReportOrderRequest>
{
    public override void Configure()
    {
        Patch("report");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("11. Report")
            .WithDescription("Set an Order's Status to Reported by specifying its Id")
        );
    }

    public override async Task HandleAsync(ReportOrderRequest req, CancellationToken ct)
    {
        ReportOrderCommand command = new(
            Id: new OrderId(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
