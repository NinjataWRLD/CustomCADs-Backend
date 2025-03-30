using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

using static ActiveCartsData;

public class ToggleActiveCartItemForDeliveryHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ProductId productId1 = ProductId.New();
    private static readonly ProductId productId2 = ProductId.New();

    public ToggleActiveCartItemForDeliveryHandlerUnitTests()
    {
        reads.Setup(x => x.SingleAsync(buyerId, productId1, true, ct))
            .ReturnsAsync(CreateItemWithDelivery(buyerId, productId1));

        reads.Setup(x => x.SingleAsync(buyerId, productId2, true, ct))
            .ReturnsAsync(CreateItem(buyerId, productId2));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, productId1, null);
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleAsync(buyerId, productId1, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, productId1, null);
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleAsync(buyerId, productId1, true, ct))
            .ReturnsAsync(null as ActiveCartItem);

        ToggleActiveCartItemForDeliveryCommand command = new(buyerId, productId1, null);
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenItemNotFound()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(
            BuyerId: buyerId,
            ProductId: ValidProductId1,
            CustomizationId: null
        );
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDeliveryMismatch()
    {
        // Arrange
        ToggleActiveCartItemForDeliveryCommand command = new(
            BuyerId: buyerId,
            ProductId: productId2,
            CustomizationId: null
        );
        ToggleActiveCartItemForDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
