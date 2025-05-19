using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Image;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Image;

using static ProductsData;

public class GetProductImagePresignedUrlGetHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Product product = CreateProduct().SetValidatedStatus();
	private static readonly DownloadFileResponse image = new("presigned-url", "application/png");

	public GetProductImagePresignedUrlGetHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetImagePresignedUrlGetByIdQuery>(), ct))
			.ReturnsAsync(image);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GalleryGetProductImagePresignedUrlGetQuery query = new(ValidId);
		GalleryGetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GalleryGetProductImagePresignedUrlGetQuery query = new(ValidId);
		GalleryGetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetImagePresignedUrlGetByIdQuery>()
		, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		GalleryGetProductImagePresignedUrlGetQuery query = new(ValidId);
		GalleryGetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(image, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotValidated()
	{
		// Arrange
		product.SetUncheckedStatus();

		GalleryGetProductImagePresignedUrlGetQuery query = new(ValidId);
		GalleryGetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

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

		GalleryGetProductImagePresignedUrlGetQuery query = new(ValidId);
		GalleryGetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
		{
			// Act
			await handler.Handle(query, ct);
		});
	}
}
