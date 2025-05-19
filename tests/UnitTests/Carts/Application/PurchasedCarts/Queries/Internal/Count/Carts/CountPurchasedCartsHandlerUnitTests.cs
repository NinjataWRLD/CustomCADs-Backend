using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts.Data;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;

using static PurchasedCartsData;

public class CountPurchasedCartsHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Dictionary<AccountId, int> counts = new()
	{
		[ValidBuyerId1] = 1,
		[ValidBuyerId2] = 2,
	};

	public CountPurchasedCartsHandlerUnitTests()
	{
		reads.Setup(x => x.CountAsync(ValidBuyerId1, ct))
			.ReturnsAsync(counts[ValidBuyerId1]);

		reads.Setup(x => x.CountAsync(ValidBuyerId2, ct))
			.ReturnsAsync(counts[ValidBuyerId2]);
	}

	[Theory]
	[ClassData(typeof(CountPurchasedCartsValidData))]
	public async Task Handle_ShouldQueryDatabase(AccountId buyerId)
	{
		// Arrange
		CountPurchasedCartsQuery query = new(buyerId);
		CountPurchasedCartsHandler handler = new(reads.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountAsync(buyerId, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CountPurchasedCartsValidData))]
	public async Task Handle_ShouldReturnProperly(AccountId buyerId)
	{
		// Arrange
		CountPurchasedCartsQuery query = new(buyerId);
		CountPurchasedCartsHandler handler = new(reads.Object);

		// Act
		int count = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(counts[buyerId], count);
	}
}
