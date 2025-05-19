using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items.Data;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;

using static PurchasedCartsData;

public class CountPurchasedCartItemsHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Dictionary<AccountId, Dictionary<PurchasedCartId, int>> counts = new()
	{
		[ValidBuyerId1] = new() { [ValidId1] = 1, [ValidId2] = 2 },
		[ValidBuyerId2] = new() { [ValidId1] = 2, [ValidId2] = 1 },
	};

	public CountPurchasedCartItemsHandlerUnitTests()
	{
		reads.Setup(x => x.CountItemsAsync(ValidBuyerId1, ct))
			.ReturnsAsync(counts[ValidBuyerId1]);

		reads.Setup(x => x.CountItemsAsync(ValidBuyerId2, ct))
			.ReturnsAsync(counts[ValidBuyerId2]);
	}

	[Theory]
	[ClassData(typeof(CountPurchasedCartItemsValidData))]
	public async Task Handle_ShouldQueryDatabase(AccountId buyerId)
	{
		// Arrange
		CountPurchasedCartItemsQuery query = new(buyerId);
		CountPurchasedCartItemsHandler handler = new(reads.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountItemsAsync(buyerId, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CountPurchasedCartItemsValidData))]
	public async Task Handle_ShouldReturnProperly(AccountId buyerId)
	{
		// Arrange
		CountPurchasedCartItemsQuery query = new(buyerId);
		CountPurchasedCartItemsHandler handler = new(reads.Object);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(counts[buyerId], result);
	}
}
