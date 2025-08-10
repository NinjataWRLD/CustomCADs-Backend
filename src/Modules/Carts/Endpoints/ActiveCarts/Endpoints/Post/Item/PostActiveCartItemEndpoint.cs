using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;
using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.Item;

public sealed class PostActiveCartItemEndpoint(IRequestSender sender)
	: Endpoint<PostActiveCartItemRequest, ActiveCartItemResponse>
{
	public override void Configure()
	{
		Post("");
		Group<ActiveCartsGroup>();
		Description(d => d
			.WithSummary("Add Item")
			.WithDescription("Add an Item to your Cart")
		);
	}

	public override async Task HandleAsync(PostActiveCartItemRequest req, CancellationToken ct)
	{
		AccountId buyerId = User.GetAccountId();

		await sender.SendCommandAsync(
			new AddActiveCartItemCommand(
				ProductId: ProductId.New(req.ProductId),
				ForDelivery: req.ForDelivery,
				CustomizationId: CustomizationId.New(req.CustomizationId),
				BuyerId: buyerId
			),
			ct
		).ConfigureAwait(false);

		ActiveCartItemDto item = await sender.SendQueryAsync(
			new GetActiveCartItemQuery(
				BuyerId: buyerId,
				ProductId: ProductId.New(req.ProductId)
			),
			ct
		).ConfigureAwait(false);

		ActiveCartItemResponse response = item.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
