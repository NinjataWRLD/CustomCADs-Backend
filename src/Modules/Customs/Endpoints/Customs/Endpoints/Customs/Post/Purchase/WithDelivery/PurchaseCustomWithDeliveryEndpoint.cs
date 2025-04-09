using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Post.Purchase.WithDelivery;

public sealed class PurchaseCustomWithDeliveryEndpoint(IRequestSender sender)
    : Endpoint<PurchasCustomWithDeliveryRequest, string>
{
    public override void Configure()
    {
        Post("purchase-delivery");
        Group<CustomerGroup>();
        Description(d => d
            .WithSummary("Purchase (ForDelivery)")
            .WithDescription("Purchase a Custom with ForDelivery")
        );
    }

    public override async Task HandleAsync(PurchasCustomWithDeliveryRequest req, CancellationToken ct)
    {
        string message = await sender.SendCommandAsync(
            new PurchaseCustomWithDeliveryCommand(
                Id: CustomId.New(req.Id),
                PaymentMethodId: req.PaymentMethodId,
                ShipmentService: req.ShipmentService,
                Count: req.Count,
                Address: req.Address,
                Contact: req.Contact,
                BuyerId: User.GetAccountId(),
                CustomizationId: CustomizationId.New(req.CustomizationId)
            ),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
