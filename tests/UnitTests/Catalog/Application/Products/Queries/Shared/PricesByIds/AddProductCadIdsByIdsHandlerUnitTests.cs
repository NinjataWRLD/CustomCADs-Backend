﻿using CustomCADs.Catalog.Application.Products.Queries.Shared;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.PricesByIds;

using static ProductsData;

public class GetProductPricesByIdsHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly GetProductPricesByIdsHandler handler;
	private readonly Mock<IProductReads> reads = new();

	private readonly ProductId[] ids = [ValidId, ValidId, ValidId];
	private readonly ProductQuery query;
	private readonly Result<Product> result;
	private readonly Product[] products = [
		CreateProductWithId(MinValidName, MinValidDescription, MinValidPrice),
		CreateProductWithId(MaxValidName, MaxValidDescription, MaxValidPrice)
	];

	public GetProductPricesByIdsHandlerUnitTests()
	{
		handler = new(reads.Object);

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
		GetProductPricesByIdsQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(this.query, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetProductPricesByIdsQuery query = new(ids);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.True(result.ElementAt(0).Value == MinValidPrice),
			() => Assert.True(result.ElementAt(1).Value == MaxValidPrice)
		);
	}
}
