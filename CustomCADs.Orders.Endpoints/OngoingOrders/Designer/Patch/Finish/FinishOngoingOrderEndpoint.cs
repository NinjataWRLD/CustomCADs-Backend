using CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Patch.Finish;

public sealed class FinishOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<FinishOngoingOrderRequest>
{
    public override void Configure()
    {
        Patch("finish");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("09. Finish")
            .WithDescription("Set an Order's Status to Finished by specifying its Id, and if its a Digital Delivery then the Cad's Key and Content Type")
        );
    }

    public override async Task HandleAsync(FinishOngoingOrderRequest req, CancellationToken ct)
    {
        FinishOngoingOrderCommand command = new(
            Id: OngoingOrderId.New(req.Id),
            DesignerId: User.GetAccountId(),
            Cad: req.ToCadDto()
        );
        FinishOngoingOrderDto dto = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        FinishOngoingOrderResponse response = dto.ToFinishOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
