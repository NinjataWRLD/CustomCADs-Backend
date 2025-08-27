using CustomCADs.Files.Application.Images.Queries.Shared;
using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlPut;

using static ImagesData;

public class GetImagePresignedUrlPutByIdHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly GetImagePresignedUrlPutByIdHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private const string PresignedUrl = "presigned-url";
	private static readonly Image image = CreateImage();
	private static readonly UploadFileRequest req = new(ValidContentType, "Batman.glb");

	public GetImagePresignedUrlPutByIdHandlerUnitTests()
	{
		handler = new(reads.Object, storage.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Image>>>()
		)).ReturnsAsync(CreateImage());

		storage.Setup(x => x.GetPresignedPutUrlAsync(image.Key, req)).ReturnsAsync(PresignedUrl);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetImagePresignedUrlPutByIdQuery query = new(ValidId, req);

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
		GetImagePresignedUrlPutByIdQuery query = new(ValidId, req);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPutUrlAsync(image.Key, req), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetImagePresignedUrlPutByIdQuery query = new(ValidId, req);

		// Act
		string url = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(PresignedUrl, url);
	}
}
