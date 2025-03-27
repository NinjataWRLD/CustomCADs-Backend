using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Report;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Patch.Report;

public sealed class ReportOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<ReportOngoingOrderRequest>
{
    public override void Configure()
    {
        Patch("report");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Report")
            .WithDescription("Set an Order's Status to Reported by specifying its Id")
        );
    }

    public override async Task HandleAsync(ReportOngoingOrderRequest req, CancellationToken ct)
    {
        ReportOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
