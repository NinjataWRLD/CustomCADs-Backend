using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create;

using Data;
using static PurchasedCartsData.CartItemsData;

public class PurchasedCartItemCreateUnitTests : PurchasedCartItemsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(PurchasedCartItemCreateValidData))]
	public void Create_ShouldNotThrow_WhenCartIsValid(decimal price, int quantity, bool forDelivery)
	{
		CreateItem(
			cartId: PurchasedCartsData.ValidId,
			productId: ValidProductId,
			cadId: ValidCadId,
			customizationId: ValidCustomizationId,
			price: price,
			quantity: quantity,
			forDelivery: forDelivery
		);
	}

	[Theory]
	[ClassData(typeof(PurchasedCartItemCreateValidData))]
	public void Create_ShouldPopulateProperties(decimal price, int quantity, bool forDelivery)
	{
		var item = CreateItem(
			cartId: PurchasedCartsData.ValidId,
			productId: ValidProductId,
			cadId: ValidCadId,
			customizationId: ValidCustomizationId,
			price: price,
			quantity: quantity,
			forDelivery: forDelivery
		);

		Assert.Multiple(
			() => Assert.Equal(PurchasedCartsData.ValidId, item.CartId),
			() => Assert.Equal(ValidProductId, item.ProductId),
			() => Assert.Equal(ValidCadId, item.CadId),
			() => Assert.Equal(ValidCustomizationId, item.CustomizationId),
			() => Assert.Equal(price, item.Price),
			() => Assert.Equal(quantity, item.Quantity),
			() => Assert.Equal(forDelivery, item.ForDelivery)
		);
	}

	[Theory]
	[ClassData(typeof(PurchasedCartItemCreateInvalidQuantityData))]
	[ClassData(typeof(PurchasedCartItemCreateInvalidPriceData))]
	public void Create_ShouldThrow_WhenCartIsNotValid(decimal price, int quantity, bool forDelivery)
	{
		Assert.Throws<CustomValidationException<PurchasedCartItem>>(
			() => CreateItem(
				cartId: PurchasedCartsData.ValidId,
				productId: ValidProductId,
				cadId: ValidCadId,
				customizationId: ValidCustomizationId,
				price: price,
				quantity: quantity,
				forDelivery: forDelivery
			)
		);
	}
}
