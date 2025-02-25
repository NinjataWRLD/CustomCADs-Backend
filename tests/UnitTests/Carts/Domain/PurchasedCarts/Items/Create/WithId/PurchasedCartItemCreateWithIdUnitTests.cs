using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.CartItems;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.WithId.Data;

namespace CustomCADs.UnitTests.Carts.Domain.PurchasedCarts.Items.Create.WithId;

public class PurchasedCartItemCreateWithIdUnitTests : PurchasedCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(PurchasedCartItemCreateWithIdValidData))]
    public void Create_ShouldNotThrow_WhenCartIsValid(PurchasedCartItemId id, PurchasedCartId cartId, ProductId productId, CadId cadId, decimal price, int quantity, bool forDelivery)
    {
        CreateItemWithId(
            id: id,
            cartId: cartId,
            productId: productId,
            cadId: cadId,
            price: price,
            quantity: quantity,
            forDelivery: forDelivery
        );
    }

    [Theory]
    [ClassData(typeof(PurchasedCartItemCreateWithIdValidData))]
    public void Create_ShouldPopulateProperties(PurchasedCartItemId id, PurchasedCartId cartId, ProductId productId, CadId cadId, decimal price, int quantity, bool forDelivery)
    {
        var item = CreateItemWithId(
            id: id,
            cartId: cartId,
            productId: productId,
            cadId: cadId,
            price: price,
            quantity: quantity,
            forDelivery: forDelivery
        );

        Assert.Multiple(
            () => Assert.Equal(cartId, item.CartId),
            () => Assert.Equal(productId, item.ProductId),
            () => Assert.Equal(cadId, item.CadId),
            () => Assert.Equal(price, item.Price),
            () => Assert.Equal(quantity, item.Quantity),
            () => Assert.Equal(forDelivery, item.ForDelivery)
        );
    }

    [Theory]
    [ClassData(typeof(PurchasedCartItemCreateWithIdInvalidQuantityData))]
    [ClassData(typeof(PurchasedCartItemCreateWithIdInvalidPriceData))]
    public void Create_ShouldThrow_WhenCartIsNotValid(PurchasedCartItemId id, PurchasedCartId cartId, ProductId productId, CadId cadId, decimal price, int quantity, bool forDelivery)
    {
        Assert.Throws<PurchasedCartItemValidationException>(() =>
        {
            CreateItemWithId(
                id: id,
                cartId: cartId,
                productId: productId,
                cadId: cadId,
                price: price,
                quantity: quantity,
                forDelivery: forDelivery
            );
        });
    }
}
