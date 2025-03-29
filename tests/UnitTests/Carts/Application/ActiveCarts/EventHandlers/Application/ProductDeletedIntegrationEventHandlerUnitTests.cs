using CustomCADs.Carts.Application.ActiveCarts.EventHandlers.Application;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Files;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.EventHandlers.Application;

using static ActiveCartsData;

public class ProductDeletedIntegrationEventHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IUnitOfWork> uow = new();

    [Fact]
    public async Task Handle_ShouldBulkDelete_WhenThresholdReached()
    {
        // Arrange
        ProductDeletedApplicationEvent ie = new(
            Id: ValidProductId1,
            ImageId: default,
            CadId: default
        );
        ProductDeletedHandler handler = new(uow.Object);

        // Act
        await handler.Handle(ie);

        // Assert
        uow.Verify(x => x.BulkDeleteItemsByProductIdAsync(ValidProductId1, ct), Times.Once);
    }
}
