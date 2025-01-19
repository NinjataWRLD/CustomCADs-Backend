using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.SetDelivery;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.SetForDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.SetForDelivery;

using static ActiveCartsData;

public class SetActiveCartItemForDeliveryHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ActiveCartItemId itemId = ActiveCartItemId.New(Guid.Empty);
    private readonly ActiveCartItem item;
    private readonly ActiveCart cart;

    public SetActiveCartItemForDeliveryHandlerUnitTests()
    {
        item = CreateItemWithId(itemId);
        cart = CreateCartWithItems(
           buyerId: buyerId,
           items: [item]
       );

        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);
    }

    [Theory]
    [ClassData(typeof(SetActiveCartItemForDeliveryValidData))]
    public async Task Handle_ShouldQueryDatabase(bool value)
    {
        // Arrange
        SetActiveCartItemForDeliveryCommand command = new(buyerId, itemId, value);
        SetActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetActiveCartItemForDeliveryValidData))]
    public async Task Handle_ShouldPersistToDatabase(bool value)
    {
        // Arrange
        SetActiveCartItemForDeliveryCommand command = new(buyerId, itemId, value);
        SetActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(SetActiveCartItemForDeliveryValidData))]
    public async Task Handle_ShouldThrowException_WhenCartNotFound(bool value)
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(null as ActiveCart);

        SetActiveCartItemForDeliveryCommand command = new(buyerId, itemId, value);
        SetActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Theory]
    [ClassData(typeof(SetActiveCartItemForDeliveryValidData))]
    public async Task Handle_ShouldThrowException_WhenItemNotFound(bool value)
    {
        // Arrange
        SetActiveCartItemForDeliveryCommand command = new(
            BuyerId: buyerId,
            ItemId: CartItemsData.ValidId1,
            Value: value
        );
        SetActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartItemNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
