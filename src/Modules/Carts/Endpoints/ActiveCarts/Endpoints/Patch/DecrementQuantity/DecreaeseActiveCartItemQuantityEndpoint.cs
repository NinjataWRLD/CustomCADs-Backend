using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Patch.DecrementQuantity;

public class DecreaeseActiveCartItemQuantityEndpoint(IRequestSender sender)
	: Endpoint<DecreaseActiveCartItemQuantityRequest>
{
	public override void Configure()
	{
		Patch("decrease");
		Group<ActiveCartsGroup>();
		Description(d => d
			.WithSummary("Decrease")
			.WithDescription("Decrease the Cart Item's quantity")
		);
	}

	public override async Task HandleAsync(DecreaseActiveCartItemQuantityRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new DecreaseActiveCartItemQuantityCommand(
				BuyerId: User.GetAccountId(),
				ProductId: ProductId.New(req.ProductId),
				Amount: req.Amount
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
