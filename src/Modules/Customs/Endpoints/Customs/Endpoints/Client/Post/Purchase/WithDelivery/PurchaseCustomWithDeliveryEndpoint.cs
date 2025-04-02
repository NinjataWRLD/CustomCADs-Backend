using CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Purchase.WithDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Post.Purchase.WithDelivery;

public sealed class PurchaseCustomWithDeliveryEndpoint(IRequestSender sender)
    : Endpoint<PurchasCustomWithDeliveryRequest, string>
{
    public override void Configure()
    {
        Post("purchase-delivery");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Purchase (ForDelivery)")
            .WithDescription("Purchase a Custom with ForDelivery")
        );
    }

    public override async Task HandleAsync(PurchasCustomWithDeliveryRequest req, CancellationToken ct)
    {
        PurchaseCustomWithDeliveryCommand command = new(
            Id: CustomId.New(req.Id),
            PaymentMethodId: req.PaymentMethodId,
            ShipmentService: req.ShipmentService,
            Count: req.Count,
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: User.GetAccountId(),
            CustomizationId: CustomizationId.New(req.CustomizationId)
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
