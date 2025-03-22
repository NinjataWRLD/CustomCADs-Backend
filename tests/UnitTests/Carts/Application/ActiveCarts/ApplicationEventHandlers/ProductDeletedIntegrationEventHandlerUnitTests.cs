using CustomCADs.Carts.Application.ActiveCarts.ApplicationEventHandlers;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.ApplicationEvents.Files;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.ApplicationEventHandlers;

using static ActiveCartConstants;
using static ActiveCartsData;

public class ProductDeletedIntegrationEventHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly ActiveCartQuery query;
    private readonly ActiveCart[] carts = [
        CreateCart(),
        CreateCart(),
        CreateCart(),
    ];
    private readonly ProductId productId = CartItemsData.ValidProductId1;

    public ProductDeletedIntegrationEventHandlerUnitTests()
    {
        query = new(
            Pagination: new(1, carts.Length),
            ProductId: productId
        );

        reads.Setup(x => x.CountByProductIdAsync(productId, ct))
            .ReturnsAsync(BulkDeleteThreshold - 1);

        reads.Setup(x => x.AllAsync(
            It.Is<ActiveCartQuery>(q => q.ProductId == productId),
            true,
            ct
        )).ReturnsAsync(new Result<ActiveCart>(carts.Length, carts));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase_WhenThresholdNotReached()
    {
        // Arrange
        ProductDeletedApplicationEvent ie = new(
            Id: productId,
            ImageId: default,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        reads.Verify(x => x.AllAsync(
            It.Is<ActiveCartQuery>(q => q.ProductId == productId), true, ct)
        , Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotQueryDatabase_WhenThresholdReached()
    {
        // Arrange
        reads.Setup(x => x.CountByProductIdAsync(productId, ct))
            .ReturnsAsync(BulkDeleteThreshold + 1);

        ProductDeletedApplicationEvent ie = new(
            Id: productId,
            ImageId: default,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        reads.Verify(x => x.AllAsync(
            It.IsAny<ActiveCartQuery>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
        ), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase_WhenThresholdNotReached()
    {
        // Arrange
        ProductDeletedApplicationEvent ie = new(
            Id: productId,
            ImageId: default,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldBulkDelete_WhenThresholdReached()
    {
        // Arrange
        reads.Setup(x => x.CountByProductIdAsync(productId, ct))
            .ReturnsAsync(BulkDeleteThreshold + 1);

        ProductDeletedApplicationEvent ie = new(
            Id: productId,
            ImageId: default,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        uow.Verify(x => x.BulkDeleteItemsByProductIdAsync(productId, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldDoNothing_WhenNoCartsAssociated()
    {
        // Arrange
        reads.Setup(x => x.CountByProductIdAsync(productId, ct))
            .ReturnsAsync(0);

        ProductDeletedApplicationEvent ie = new(
            Id: productId,
            ImageId: default,
            CadId: default
        );
        ProductDeletedHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        reads.Verify(x => x.AllAsync(
            It.IsAny<ActiveCartQuery>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
        ), Times.Never);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Never);
    }
}
