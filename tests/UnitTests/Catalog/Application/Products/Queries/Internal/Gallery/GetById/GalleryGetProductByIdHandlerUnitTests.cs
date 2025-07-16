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
	private readonly GalleryGetProductByIdHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Product product = CreateProductWithId(id: ValidId);

	public GalleryGetProductByIdHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object, raiser.Object);

		product.SetValidatedStatus();
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		CoordinatesDto coords = new(0, 0, 0);
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadCoordsByIdQuery>(x => x.Id == product.CadId),
			ct
		)).ReturnsAsync(new GetCadCoordsByIdDto(coords, coords));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCadVolumeByIdQuery>(x => x.Id == product.CadId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == product.CreatorId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryNameByIdQuery>(x => x.Id == product.CategoryId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCadCoordsByIdQuery>(x => x.Id == product.CadId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents_WhenAccountIdEmpty()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.Is<ProductViewedDomainEvent>(x => x.Id == product.Id)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldNotRaiseEvents_WhenAccountIdEmpty()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, AccountId.New(Guid.Empty));

		// Act
		await handler.Handle(query, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.Is<ProductViewedDomainEvent>(x => x.Id == product.Id)
		), Times.Never());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);

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

		// Assert
		await Assert.ThrowsAsync<CustomStatusException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Product);
		GalleryGetProductByIdQuery query = new(ValidId, ValidCreatorId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
