using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrease;

using static ActiveCartItemConstants;
using static ActiveCartsData;

public class DecreaseActiveCartItemQuantityHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly DecreaseActiveCartItemQuantityHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private int oldQuantity;

	public DecreaseActiveCartItemQuantityHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		var item = CreateItemWithDelivery(productId: ValidProductId).IncreaseQuantity(QuantityMax - 1);
		oldQuantity = item.Quantity;

		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
			.ReturnsAsync(item);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DecreaseActiveCartItemQuantityCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: ValidProductId,
			Amount: MinValidQuantity
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DecreaseActiveCartItemQuantityCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: ValidProductId,
			Amount: MinValidQuantity
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		DecreaseActiveCartItemQuantityCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: ValidProductId,
			Amount: MinValidQuantity
		);

		// Act
		int result = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(oldQuantity - command.Amount, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleAsync(ValidBuyerId, ValidProductId, true, ct))
			.ReturnsAsync(null as ActiveCartItem);

		DecreaseActiveCartItemQuantityCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: ValidProductId,
			Amount: MinValidQuantity
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenItemNotFound()
	{
		// Arrange
		DecreaseActiveCartItemQuantityCommand command = new(
			BuyerId: ValidBuyerId,
			ProductId: ProductId.New(),
			Amount: MinValidQuantity
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
