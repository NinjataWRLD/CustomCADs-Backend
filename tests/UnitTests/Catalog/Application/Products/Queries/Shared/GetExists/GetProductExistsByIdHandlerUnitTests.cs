using CustomCADs.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetExists;

public class GetProductExistsByIdHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly GetProductExistsByIdHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private static readonly ProductId id = new();

	public GetProductExistsByIdHandlerUnitTests()
	{
		handler = new(reads.Object);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);
		GetProductExistsByIdQuery query = new(id);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ExistsByIdAsync(id, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly_WhenProductExists()
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(true);
		GetProductExistsByIdQuery query = new(id);

		// Act
		bool exists = await handler.Handle(query, ct);

		// Assert
		Assert.True(exists);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly_WhenProductDoesNotExists()
	{
		// Arrange
		reads.Setup(x => x.ExistsByIdAsync(id, ct)).ReturnsAsync(false);
		GetProductExistsByIdQuery query = new(id);

		// Act
		bool exists = await handler.Handle(query, ct);

		// Assert
		Assert.False(exists);
	}
}
