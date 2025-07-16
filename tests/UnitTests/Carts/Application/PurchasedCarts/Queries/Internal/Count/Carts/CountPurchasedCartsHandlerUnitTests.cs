using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;

using static PurchasedCartsData;

public class CountPurchasedCartsHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly CountPurchasedCartsHandler handler;
	private readonly Mock<IPurchasedCartReads> reads = new();

	private const int Count = 4;

	public CountPurchasedCartsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountAsync(ValidBuyerId, ct))
			.ReturnsAsync(Count);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CountPurchasedCartsQuery query = new(ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountAsync(ValidBuyerId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CountPurchasedCartsQuery query = new(ValidBuyerId);

		// Act
		int count = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(Count, count);
	}
}
