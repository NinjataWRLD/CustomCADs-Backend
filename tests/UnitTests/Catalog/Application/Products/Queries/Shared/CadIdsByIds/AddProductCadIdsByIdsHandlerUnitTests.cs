using CustomCADs.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.CadIdsByIds;

using static ProductsData;

public class GetProductCadIdsByIdsHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly Mock<IProductReads> reads = new();
	private readonly ProductId[] ids = [ValidId, ValidId, ValidId];
	private readonly ProductQuery query;
	private readonly Result<Product> result;
	private readonly Product[] products = [
		CreateProductWithId(ValidName1, ValidDescription1, ValidPrice1),
		CreateProductWithId(ValidName2, ValidDescription2, ValidPrice2)
	];

	public GetProductCadIdsByIdsHandlerUnitTests()
	{
		query = new(
			Ids: ids,
			Pagination: new(Limit: ids.Length)
		);
		result = new(
			Count: products.Length,
			Items: products
		);
		reads.Setup(x => x.AllAsync(query, false, ct))
			.ReturnsAsync(result);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetProductCadIdsByIdsQuery query = new(ids);
		GetProductCadIdsByIdsHandler handler = new(reads.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		GetProductCadIdsByIdsQuery query = new(ids);
		GetProductCadIdsByIdsHandler handler = new(reads.Object);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.True(result.ElementAt(0).Value == ValidCadId),
			() => Assert.True(result.ElementAt(1).Value == ValidCadId)
		);
	}
}
