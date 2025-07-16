using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Get;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlGetHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductImagePresignedUrlGetHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly DownloadFileResponse image = new("presigned-url", "application/png");
	private readonly Product product = CreateProduct(creatorId: ValidDesignerId);

	public CreatorGetProductImagePresignedUrlGetHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlGetByIdQuery>(x => x.Id == product.ImageId),
			ct
		)).ReturnsAsync(image);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlGetQuery query = new(ValidId, ValidDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlGetQuery query = new(ValidId, ValidDesignerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlGetByIdQuery>(x => x.Id == product.ImageId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlGetQuery query = new(ValidId, ValidDesignerId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(image, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlGetQuery query = new(ValidId, ValidCreatorId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(
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
		CreatorGetProductImagePresignedUrlGetQuery query = new(ValidId, ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
