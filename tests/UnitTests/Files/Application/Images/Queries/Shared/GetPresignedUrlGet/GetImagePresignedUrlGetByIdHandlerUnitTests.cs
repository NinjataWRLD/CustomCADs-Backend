using CustomCADs.Files.Application.Images.Queries.Shared;
using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlGet;

using static ImagesData;

public class GetImagePresignedUrlGetByIdHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly GetImagePresignedUrlGetByIdHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private static readonly Image image = CreateImage();
	private const string PresignedUrl = "PresignedUrl";

	public GetImagePresignedUrlGetByIdHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Image>>>()
		)).ReturnsAsync(image);

		storage.Setup(x => x.GetPresignedGetUrlAsync(image.Key))
			.ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetImagePresignedUrlGetByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Image>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetImagePresignedUrlGetByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedGetUrlAsync(image.Key), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetImagePresignedUrlGetByIdQuery query = new(ValidId);

		// Act
		var (Url, ContentType) = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(image.ContentType, ContentType),
			() => Assert.Equal(PresignedUrl, Url)
		);
	}
}
