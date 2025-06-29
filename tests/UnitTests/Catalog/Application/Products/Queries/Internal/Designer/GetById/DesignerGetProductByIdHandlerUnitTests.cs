﻿using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Designer.GetById;

using static ProductsData;

public class DesignerGetProductByIdHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly DesignerGetProductByIdHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Product product = CreateProduct();

	public DesignerGetProductByIdHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DesignerGetProductByIdQuery query = new(ValidId, ValidDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		DesignerGetProductByIdQuery query = new(ValidId, ValidDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryNameByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		DesignerGetProductByIdQuery query = new(ValidId, ValidDesignerId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(product.Id, result.Id),
			() => Assert.Equal(product.Name, result.Name),
			() => Assert.Equal(product.Description, result.Description),
			() => Assert.Equal(product.Price, result.Price),
			() => Assert.Equal(product.CategoryId, result.Category.Id)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);
		DesignerGetProductByIdQuery query = new(ValidId, ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
