using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

using static PurchasedCartsData;

public class GetAllPurchasedCartsUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly GetAllPurchasedCartsHandler handler;
	private readonly Mock<IPurchasedCartReads> reads = new();

	private readonly PurchasedCart[] carts = [
		CreateCartWithId(id: ValidId),
		CreateCartWithId(id: ValidId),
	];
	private readonly PurchasedCartQuery query;

	public GetAllPurchasedCartsUnitTests()
	{
		handler = new(reads.Object);

		query = new(
			Pagination: new(1, carts.Length)
		);

		reads.Setup(x => x.AllAsync(query, false, ct))
			.ReturnsAsync(new Result<PurchasedCart>(
				carts.Length,
				carts
			));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAllPurchasedCartsQuery query = new(this.query.Pagination);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllPurchasedCartsQuery query = new(this.query.Pagination);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		int expectedCount = carts.Length, actualCount = result.Count;
		PurchasedCartId[] expectedIds = [.. carts.Select(x => x.Id)],
			actualIds = [.. result.Items.Select(x => x.Id)];

		Assert.Multiple(
			() => Assert.Equal(expectedCount, actualCount),
			() => Assert.Equal(expectedIds, actualIds)
		);
	}
}
