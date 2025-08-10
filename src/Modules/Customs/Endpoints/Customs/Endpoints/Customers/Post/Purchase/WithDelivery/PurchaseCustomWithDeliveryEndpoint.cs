using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using CustomCADs.Customs.Endpoints.Customs.Dtos;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Purchase.WithDelivery;

public sealed class PurchaseCustomWithDeliveryEndpoint(IRequestSender sender)
	: Endpoint<PurchasCustomWithDeliveryRequest, PaymentResponse>
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
		PaymentDto dto = await sender.SendCommandAsync(
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

		PaymentResponse response = dto.ToResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
