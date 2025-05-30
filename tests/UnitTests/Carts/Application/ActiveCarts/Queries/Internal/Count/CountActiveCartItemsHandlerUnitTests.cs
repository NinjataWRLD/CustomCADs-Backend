using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.Count;
using CustomCADs.Carts.Domain.Repositories.Reads;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.Count;

using static ActiveCartsData;

public class CountActiveCartItemsHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly CountActiveCartItemsHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();

	private const int Count = 5;

	public CountActiveCartItemsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.CountAsync(ValidBuyerId, ct))
			.ReturnsAsync(Count);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CountActiveCartItemsQuery query = new(ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.CountAsync(ValidBuyerId, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		CountActiveCartItemsQuery query = new(ValidBuyerId);

		// Act
		int count = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(Count, count);
	}
}
