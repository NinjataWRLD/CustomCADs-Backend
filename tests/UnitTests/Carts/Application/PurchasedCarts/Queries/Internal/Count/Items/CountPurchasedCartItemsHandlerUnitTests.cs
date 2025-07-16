using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;

using static PurchasedCartsData;

public class CountPurchasedCartItemsHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly CountPurchasedCartItemsHandler handler;
	private readonly Mock<IPurchasedCartReads> reads = new();

	private static readonly Dictionary<PurchasedCartId, int> count = new() { [ValidId] = 4 };

	public CountPurchasedCartItemsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountItemsAsync(ValidBuyerId, ct))
			.ReturnsAsync(count);

		reads.Setup(x => x.CountItemsAsync(ValidBuyerId, ct))
			.ReturnsAsync(count);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CountPurchasedCartItemsQuery query = new(ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountItemsAsync(ValidBuyerId, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CountPurchasedCartItemsQuery query = new(ValidBuyerId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(count, result);
	}
}
