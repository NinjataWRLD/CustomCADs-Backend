﻿using CustomCADs.Catalog.Application.Products.Events.Domain;
using CustomCADs.Catalog.Domain.Products.Events;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Catalog;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Domain;

using static ProductsData;

public class ProductViewedHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly ProductViewedHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Product product = CreateProduct();

	public ProductViewedHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(product);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ProductViewedDomainEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductViewedDomainEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		ProductViewedDomainEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ProductViewedDomainEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserViewedProductApplicationEvent>(x => x.Id == ValidId && x.AccountId == ValidCreatorId)
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		ProductViewedDomainEvent de = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(de);

		// Assert
		Assert.Equal(1, product.Counts.Views);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Product);

		ProductViewedDomainEvent de = new(ValidId, ValidCreatorId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(de)
		);
	}
}
