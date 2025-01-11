using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Add;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Add.Data;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Add;

public class AddActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private ActiveCart cart = CreateCart();

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldQueryDatabase(AccountId buyerId, double weight, bool forDelivery, ProductId productId)
    {
        // Arrange
        cart = CreateCart(buyerId);
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            Weight: weight,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, true, ct));
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldPersistToDatabase(AccountId buyerId, double weight, bool forDelivery, ProductId productId)
    {
        // Arrange
        cart = CreateCart(buyerId);
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            Weight: weight,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct));
    }
    
    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldPopulateProperly(AccountId buyerId, double weight, bool forDelivery, ProductId productId)
    {
        // Arrange
        cart = CreateCart(buyerId);
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            Weight: weight,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        var item = cart.Items.First();
        Assert.Multiple(
            () => Assert.Equal(weight, item.Weight),
            () => Assert.Equal(forDelivery, item.ForDelivery),
            () => Assert.Equal(productId, item.ProductId)
        );
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldThrowException_WhenCartNotFound(AccountId buyerId, double weight, bool forDelivery, ProductId productId)
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(null as ActiveCart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            Weight: weight,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
