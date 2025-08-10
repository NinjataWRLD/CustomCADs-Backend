using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

using static ActiveCartsData;

public class GetActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly GetActiveCartItemHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly AccountId newBuyerId = AccountId.New();

	public GetActiveCartItemHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleAsync(newBuyerId, ValidProductId, false, ct))
			.ReturnsAsync(CreateItem());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetActiveCartItemQuery query = new(newBuyerId, ValidProductId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleAsync(newBuyerId, ValidProductId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetActiveCartItemQuery query = new(newBuyerId, ValidProductId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidProductId, result.ProductId);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartItemNotFound()
	{
		// Arrange
		GetActiveCartItemQuery query = new(newBuyerId, ProductId.New());

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		GetActiveCartItemQuery query = new(newBuyerId, ProductId.New());

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
