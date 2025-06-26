using CustomCADs.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;

using static AccountsData;

public class GetAccountViewedProductHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAccountViewedProductHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private static readonly ProductId productId = ProductId.New();

	public GetAccountViewedProductHandlerUnitTests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ViewedProductsByIdAsync(ValidId, ct))
			.ReturnsAsync([]);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAccountViewedProductQuery query = new(ValidId, productId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ViewedProductsByIdAsync(ValidId, ct), Times.Once);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_ShouldReturnResult(bool expected)
	{
		// Arrange
		if (expected)
		{
			reads.Setup(x => x.ViewedProductsByIdAsync(ValidId, ct))
				.ReturnsAsync([productId]);
		}
		GetAccountViewedProductQuery query = new(ValidId, productId);

		// Act
		bool actual = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(expected, actual);
	}
}
