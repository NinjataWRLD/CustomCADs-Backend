using CustomCADs.Orders.Application.Orders.Commands.Finish;

namespace CustomCADs.Orders.Endpoints.Designer.Patch.Finish;

public class FinishOrderEndpoint(IRequestSender sender)
    : Endpoint<FinishOrderRequest>
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

    public override async Task HandleAsync(FinishOrderRequest req, CancellationToken ct)
    {
        FinishOrderCommand command = new(
            Id: new OrderId(req.Id),
            DesignerId: User.GetAccountId(),
            Cad: req.ToCadDto()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
