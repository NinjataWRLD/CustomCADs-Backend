using CustomCADs.Files.Application.Images.Queries.Shared;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlGet;

public class GetImagePresignedUrlGetByIdHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly GetImagePresignedUrlGetByIdHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IStorageService> storage = new();

	private static readonly Image image = CreateImage();
	private const string PresignedUrl = "PresignedUrl";

	public GetImagePresignedUrlGetByIdHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object);

		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(image);

		storage.Setup(x => x.GetPresignedGetUrlAsync(image.Key, image.ContentType))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetImagePresignedUrlGetByIdQuery query = new(id);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetImagePresignedUrlGetByIdQuery query = new(id);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedGetUrlAsync(
			image.Key,
			image.ContentType
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		GetImagePresignedUrlGetByIdQuery query = new(id);

		// Act
		var (Url, ContentType) = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(image.ContentType, ContentType),
			() => Assert.Equal(PresignedUrl, Url)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenImageNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(null as Image);
		GetImagePresignedUrlGetByIdQuery query = new(id);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
