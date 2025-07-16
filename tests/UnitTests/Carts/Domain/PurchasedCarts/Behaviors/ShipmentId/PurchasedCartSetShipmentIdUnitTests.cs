using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Behaviors.ShipmentId;

using static PurchasedCartsData;

public class PurchasedCartSetShipmentIdUnitTests : PurchasedCartsBaseUnitTests
{
	[Fact]
	public void SetShipmentId_ShouldNotThrowException_WhenIsDelivery()
	{
		var items = CreateItems(1, 1);
		var cads = items.ToDictionary(x => x.ProductId, x => CadId.New());

		CreateCartWithItems(
			buyerId: ValidBuyerId,
			items: items,
			prices: items.ToDictionary(x => x.ProductId, x => CartItemsData.MinValidPrice),
			productCads: cads,
			itemCads: cads.ToDictionary(x => x.Value, x => CadId.New())
		).SetShipmentId(ValidShipmentId);
	}

	[Fact]
	public void SetShipmentId_ShouldThrowException_WhenNotDelivery()
	{
		var items = CreateItems(2, 0);
		var cads = items.ToDictionary(x => x.ProductId, x => CadId.New());

		Assert.Throws<CustomValidationException<PurchasedCart>>(
			() => CreateCartWithItems(
				buyerId: ValidBuyerId,
				items: items,
				prices: items.ToDictionary(x => x.ProductId, x => CartItemsData.MinValidPrice),
				productCads: cads,
				itemCads: cads.ToDictionary(x => x.Value, x => CadId.New())
			).SetShipmentId(ValidShipmentId)
		);
	}
}
