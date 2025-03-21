﻿using CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Begin;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Patch.Begin;

public sealed class BeginOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<BeginOngoingOrderRequest>
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

    public override async Task HandleAsync(BeginOngoingOrderRequest req, CancellationToken ct)
    {
        BeginOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
