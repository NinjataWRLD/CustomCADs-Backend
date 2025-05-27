using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;
using CustomCADs.Catalog.Domain.Products.Events;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

using static ProductsData;

public class GalleryGetProductByIdHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Product product = CreateProduct();

	public GalleryGetProductByIdHandlerUnitTests()
	{
		product.SetValidatedStatus();

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		CoordinatesDto coords = new(0, 0, 0);
		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadCoordsByIdQuery>(), ct))
			.ReturnsAsync(new GetCadCoordsByIdDto(coords, coords));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatbase()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);
		GalleryGetProductByIdHandler handler = new(reads.Object, sender.Object, raiser.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);
		GalleryGetProductByIdHandler handler = new(reads.Object, sender.Object, raiser.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(It.IsAny<GetCadVolumeByIdQuery>(), ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(It.IsAny<GetCategoryNameByIdQuery>(), ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(It.IsAny<GetCadCoordsByIdQuery>(), ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents_WhenAccountIdEmpty()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);
		GalleryGetProductByIdHandler handler = new(reads.Object, sender.Object, raiser.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.IsAny<ProductViewedDomainEvent>())
		, Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldNotRaiseEvents_WhenAccountIdEmpty()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, AccountId.New(Guid.Empty));
		GalleryGetProductByIdHandler handler = new(reads.Object, sender.Object, raiser.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.IsAny<ProductViewedDomainEvent>())
		, Times.Never);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);
		GalleryGetProductByIdHandler handler = new(reads.Object, sender.Object, raiser.Object);

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
	public async Task Handle_ShouldThrowException_WhenStatusIsNotValid()
	{
		// Arrange
		product.SetReportedStatus();

		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);
		GalleryGetProductByIdHandler handler = new(reads.Object, sender.Object, raiser.Object);

		// Assert
		await Assert.ThrowsAsync<CustomStatusException<Product>>(async () =>
		{
			// Act
			await handler.Handle(query, ct);
		});
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);

		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);
		GalleryGetProductByIdHandler handler = new(reads.Object, sender.Object, raiser.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
		{
			// Act
			await handler.Handle(query, ct);
		});
	}
}
