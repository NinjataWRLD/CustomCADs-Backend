using CustomCADs.Carts.Domain.PurchasedCarts.Exceptions.CartItems;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.Data;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create;

public class PurchasedCartItemCreateUnitTests : PurchasedCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(PurchasedCartItemCreateValidData))]
    public void Create_ShouldNotThrow_WhenCartIsValid(PurchasedCartId cartId, ProductId productId, CadId cadId, CustomizationId? customizationId, decimal price, int quantity, bool forDelivery)
    {
        CreateItem(
            cartId: cartId,
            productId: productId,
            cadId: cadId,
            customizationId: customizationId,
            price: price,
            quantity: quantity,
            forDelivery: forDelivery
        );
    }

    [Theory]
    [ClassData(typeof(PurchasedCartItemCreateValidData))]
    public void Create_ShouldPopulateProperties(PurchasedCartId cartId, ProductId productId, CadId cadId, CustomizationId? customizationId, decimal price, int quantity, bool forDelivery)
    {
        var item = CreateItem(
            cartId: cartId,
            productId: productId,
            cadId: cadId,
            customizationId: customizationId,
            price: price,
            quantity: quantity,
            forDelivery: forDelivery
        );

        Assert.Multiple(
            () => Assert.Equal(cartId, item.CartId),
            () => Assert.Equal(productId, item.ProductId),
            () => Assert.Equal(cadId, item.CadId),
            () => Assert.Equal(customizationId, item.CustomizationId),
            () => Assert.Equal(price, item.Price),
            () => Assert.Equal(quantity, item.Quantity),
            () => Assert.Equal(forDelivery, item.ForDelivery)
        );
    }

    [Theory]
    [ClassData(typeof(PurchasedCartItemCreateInvalidQuantityData))]
    [ClassData(typeof(PurchasedCartItemCreateInvalidPriceData))]
    public void Create_ShouldThrow_WhenCartIsNotValid(PurchasedCartId cartId, ProductId productId, CadId cadId, CustomizationId? customizationId, decimal price, int quantity, bool forDelivery)
    {
        Assert.Throws<PurchasedCartItemValidationException>(() =>
        {
            CreateItem(
                cartId: cartId,
                productId: productId,
                cadId: cadId,
                customizationId: customizationId,
                price: price,
                quantity: quantity,
                forDelivery: forDelivery
            );
        });
    }
}
