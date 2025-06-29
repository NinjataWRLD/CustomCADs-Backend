﻿using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlPutHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductImagePresignedUrlPutHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Url = "presigned-url";
	private static readonly UploadFileRequest file = new("content-type", "file-name");
	private readonly Product product = CreateProduct();

	public CreatorGetProductImagePresignedUrlPutHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPutByIdQuery>(x => x.Id == product.ImageId && x.NewFile == file),
			ct
		)).ReturnsAsync(Url);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPutQuery query = new(
			Id: ValidId,
			NewImage: file,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPutQuery query = new(
			Id: ValidId,
			NewImage: file,
			CreatorId: ValidCreatorId
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPutByIdQuery>(x => x.Id == product.ImageId && x.NewFile == file),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPutQuery query = new(
			Id: ValidId,
			NewImage: file,
			CreatorId: ValidCreatorId
		);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(Url, result.PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPutQuery query = new(
			Id: ValidId,
			NewImage: file,
			CreatorId: ValidDesignerId
		);

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

		CreatorGetProductImagePresignedUrlPutQuery query = new(
			Id: ValidId,
			NewImage: file,
			CreatorId: ValidCreatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
