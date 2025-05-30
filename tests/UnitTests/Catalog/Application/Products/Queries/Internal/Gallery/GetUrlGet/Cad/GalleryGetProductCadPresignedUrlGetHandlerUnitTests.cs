using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Gallery.GetUrlGet.Cad;

using static ProductsData;

public class GalleryGetProductCadPresignedUrlGetHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly GalleryGetProductCadPresignedUrlGetHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly DownloadFileResponse cad = new("presigned-url", "application/png");
	private readonly Product product = CreateProduct().SetValidatedStatus();

	public GalleryGetProductCadPresignedUrlGetHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlGetByIdQuery>(x => x.Id == product.CadId),
			ct
		)).ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GalleryGetProductCadPresignedUrlGetQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GalleryGetProductCadPresignedUrlGetQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlGetByIdQuery>(x => x.Id == product.CadId),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		GalleryGetProductCadPresignedUrlGetQuery query = new(ValidId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(cad, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotValidated()
	{
		// Arrange
		product.SetUncheckedStatus();
		GalleryGetProductCadPresignedUrlGetQuery query = new(ValidId);

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
		GalleryGetProductCadPresignedUrlGetQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
