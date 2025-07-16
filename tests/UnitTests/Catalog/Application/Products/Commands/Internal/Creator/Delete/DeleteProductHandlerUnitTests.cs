using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Delete;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Files;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Delete;

using static ProductsData;

public class DeleteProductHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly DeleteProductHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Product product = CreateProduct();

	public DeleteProductHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(product);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteProductCommand command = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteProductCommand command = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(product), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		DeleteProductCommand command = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.IsAny<ProductDeletedApplicationEvent>()
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		DeleteProductCommand command = new(ValidId, ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Product);
		DeleteProductCommand command = new(ValidId, ValidCreatorId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
