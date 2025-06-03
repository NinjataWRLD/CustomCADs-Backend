using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Remove;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Delete;

public sealed class DeleteActiveCartItemEndpoint(IRequestSender sender)
	: Endpoint<DeleteActiveCartItemRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<ActiveCartsGroup>();
		Description(d => d
			.WithSummary("Remove Item")
			.WithDescription("Remove an Item from your Cart")
		);
	}

	public override async Task HandleAsync(DeleteActiveCartItemRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new RemoveActiveCartItemCommand(
				ProductId: ProductId.New(req.ProductId),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
