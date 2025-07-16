using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;

public class CreatePurchasedCartHandler(IWrites<PurchasedCart> writes, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
	: ICommandHandler<CreatePurchasedCartCommand, PurchasedCartId>
{
	public async Task<PurchasedCartId> Handle(CreatePurchasedCartCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<PurchasedCart>.ById(req.BuyerId, "User");
		}

		CartItemDto[] items = await SnapshotItemsAsync(
			items: req.Items,
			prices: req.Prices,
			ct: ct
		).ConfigureAwait(false);

		PurchasedCart cart = await writes.AddAsync(
			entity: PurchasedCart.Create(req.BuyerId).AddItems(items),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new UserPurchasedProductApplicationEvent(Ids: [.. items.Select(i => i.ProductId)])
		).ConfigureAwait(false);

		return cart.Id;
	}

	private async Task<CartItemDto[]> SnapshotItemsAsync(ActiveCartItemDto[] items, Dictionary<ProductId, decimal> prices, CancellationToken ct = default)
	{
		ProductId[] productIds = [.. items.Select(i => i.ProductId)];

		Dictionary<ProductId, CadId> productCads = await sender.SendQueryAsync(
			new GetProductCadIdsByIdsQuery(productIds),
			ct
		).ConfigureAwait(false);

		Dictionary<CadId, CadId> itemCads = await sender.SendCommandAsync(
			new DuplicateCadsByIdsCommand([.. productCads.Select(c => c.Value)]),
			ct
		).ConfigureAwait(false);

		return [.. items.Select(item =>
		{
			decimal price = prices[item.ProductId];
			CadId productCadId = productCads[item.ProductId];
			CadId itemCadId = itemCads[productCadId];

			return new CartItemDto(
				Price: price,
				CadId: itemCadId,
				ProductId: item.ProductId,
				ForDelivery: item.ForDelivery,
				CustomizationId: item.CustomizationId,
				Quantity: item.Quantity,
				AddedAt: item.AddedAt
			);
		})];
	}
}
