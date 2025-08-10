using CustomCADs.Carts.Application.ActiveCarts.Events.Application.ProductDeleted;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Events.Files;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Events.Application.ProductDeleted;

using static ActiveCartsData;

public class ProductDeletedApplicationEventHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly ProductDeletedHandler handler;
	private readonly Mock<IUnitOfWork> uow = new();

	public ProductDeletedApplicationEventHandlerUnitTests()
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
		uow.Verify(x => x.BulkDeleteItemsByProductIdAsync(ValidProductId, ct), Times.Once());
	}
}
