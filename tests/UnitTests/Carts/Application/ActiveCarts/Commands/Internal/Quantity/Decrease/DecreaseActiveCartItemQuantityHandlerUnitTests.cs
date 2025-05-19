using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrease.Data;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrease;

using static ActiveCartItemConstants;
using static ActiveCartsData;

public class DecreaseActiveCartItemQuantityHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private static readonly AccountId buyerId = ValidBuyerId1;
	private static readonly ProductId productId = ProductId.New(Guid.Empty);

	public DecreaseActiveCartItemQuantityHandlerUnitTests()
	{
		var item = CreateItemWithDelivery(productId: productId).IncreaseQuantity(QuantityMax - 1);

		reads.Setup(x => x.SingleAsync(buyerId, productId, true, ct))
			.ReturnsAsync(item);
	}

	[Theory]
	[ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
	public async Task Handle_ShouldQueryDatabase(int amount)
	{
		// Arrange
		DecreaseActiveCartItemQuantityCommand command = new(buyerId, productId, amount);
		DecreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleAsync(buyerId, productId, true, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
	public async Task Handle_ShouldPersistToDatabase(int amount)
	{
		// Arrange
		DecreaseActiveCartItemQuantityCommand command = new(buyerId, productId, amount);
		DecreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
	public async Task Handle_ShouldThrowException_WhenCartNotFound(int amount)
	{
		// Arrange
		reads.Setup(x => x.SingleAsync(buyerId, productId, true, ct))
			.ReturnsAsync(null as ActiveCartItem);

		DecreaseActiveCartItemQuantityCommand command = new(buyerId, productId, amount);
		DecreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Theory]
	[ClassData(typeof(DecreaseActiveCartItemQuantityValidData))]
	public async Task Handle_ShouldThrowException_WhenItemNotFound(int amount)
	{
		// Arrange
		DecreaseActiveCartItemQuantityCommand command = new(
			BuyerId: buyerId,
			ProductId: ValidProductId1,
			Amount: amount
		);
		DecreaseActiveCartItemQuantityHandler handler = new(reads.Object, uow.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
