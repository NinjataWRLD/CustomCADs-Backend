using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Remove;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Remove;

using static ActiveCartsData;

public class RemoveActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IWrites<ActiveCartItem>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly ActiveCartItem item = CreateItem(productId: productId);
	private static readonly AccountId buyerId = ValidBuyerId1;
	private static readonly ProductId productId = ProductId.New(Guid.Empty);

	public RemoveActiveCartItemHandlerUnitTests()
	{
		reads.Setup(x => x.SingleAsync(buyerId, productId, true, ct))
			.ReturnsAsync(item);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		RemoveActiveCartItemCommand command = new(
			BuyerId: buyerId,
			ProductId: productId
		);
		RemoveActiveCartItemHandler handler = new(reads.Object, writes.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleAsync(buyerId, productId, true, ct));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		RemoveActiveCartItemCommand command = new(
			BuyerId: buyerId,
			ProductId: productId
		);
		RemoveActiveCartItemHandler handler = new(reads.Object, writes.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct));
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleAsync(buyerId, productId, true, ct))
			.ReturnsAsync(null as ActiveCartItem);

		RemoveActiveCartItemCommand command = new(
			BuyerId: buyerId,
			ProductId: productId
		);
		RemoveActiveCartItemHandler handler = new(reads.Object, writes.Object, uow.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
