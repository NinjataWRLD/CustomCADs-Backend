using CustomCADs.Carts.Application.ActiveCarts.Events.Application;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Files;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Events.Application;

using static ActiveCartsData;

public class ProductDeletedIntegrationEventHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly ProductDeletedHandler handler;
    private readonly Mock<IUnitOfWork> uow = new();

    public ProductDeletedIntegrationEventHandlerUnitTests()
    {
        handler = new(uow.Object);
    }

    [Fact]
    public async Task Handle_ShouldBulkDelete_WhenThresholdReached()
    {
        // Arrange
        ProductDeletedApplicationEvent ie = new(
            Id: ValidProductId,
            ImageId: default,
            CadId: default
        );

        // Act
        await handler.Handle(ie);

        // Assert
        uow.Verify(x => x.BulkDeleteItemsByProductIdAsync(ValidProductId, ct), Times.Once);
    }
}
