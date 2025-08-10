using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;
using CustomCADs.Customs.Endpoints.Customs.Dtos;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Purchase.Normal;

public sealed class PurchaseCustomEndpoint(IRequestSender sender)
	: Endpoint<PurchaseCustomRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("Purchase")
			.WithDescription("Purchase a Custom")
		);
	}

	public override async Task HandleAsync(PurchaseCustomRequest req, CancellationToken ct)
	{
		PaymentDto dto = await sender.SendCommandAsync(
			new PurchaseCustomCommand(
				Id: CustomId.New(req.Id),
				PaymentMethodId: req.PaymentMethodId,
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		PaymentResponse response = dto.ToResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
