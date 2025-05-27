using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;
using CustomCADs.Shared.Abstractions.Payment;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.Purchase;

public sealed class PurchaseActiveCartEndpoint(IRequestSender sender)
	: Endpoint<PurchaseActiveCartRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase");
		Group<ActiveCartsGroup>();
		Description(d => d
			.WithSummary("Purchase")
			.WithDescription("Purchase all the Items in the Cart")
		);
	}

	public override async Task HandleAsync(PurchaseActiveCartRequest req, CancellationToken ct)
	{
		PaymentDto dto = await sender.SendCommandAsync(
			new PurchaseActiveCartCommand(
				PaymentMethodId: req.PaymentMethodId,
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		PaymentResponse response = dto.ToResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
