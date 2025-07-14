using CustomCADs.Files.Application.Images.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlPut;

using static ImagesData;

public class GetImagePresignedUrlPutByIdHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly GetImagePresignedUrlPutByIdHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IStorageService> storage = new();

	private static readonly Image image = CreateImage();
	private static readonly UploadFileRequest newFile = new(ValidContentType, "Batman.glb");

	public GetImagePresignedUrlPutByIdHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object);
		reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(image);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetImagePresignedUrlPutByIdQuery query = new(
			id,
			newFile
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCallStorage_WhenImageFound()
	{
		// Arrange
		GetImagePresignedUrlPutByIdQuery query = new(
			id,
			newFile
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPutUrlAsync(
			image.Key,
			newFile
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenImageNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct)).ReturnsAsync(null as Image);

		GetImagePresignedUrlPutByIdQuery query = new(
			id,
			newFile
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
