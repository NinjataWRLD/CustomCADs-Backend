using CustomCADs.Files.Application.Images.Events.Application;
using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Events.Files;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Files.Application.Images.Events.Application;

using static ImagesData;

public class ProductDeletedHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly ProductDeletedHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IWrites<Image>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IImageStorageService> storage = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private static readonly Image image = CreateImage();

	public ProductDeletedHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, storage.Object, cache.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(image);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: default,
			ImageId: ValidId,
			CadId: default
		);

		// Act
		await handler.Handle(ie);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: default,
			ImageId: ValidId,
			CadId: default
		);

		// Act
		await handler.Handle(ie);

		// Assert
		writes.Verify(x => x.Remove(image), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: default,
			ImageId: ValidId,
			CadId: default
		);

		// Act
		await handler.Handle(ie);

		// Assert
		cache.Verify(
			x => x.ClearAsync(ValidId),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		ProductDeletedApplicationEvent ie = new(
			Id: default,
			ImageId: ValidId,
			CadId: default
		);

		// Act
		await handler.Handle(ie);

		// Assert
		storage.Verify(x => x.DeleteFileAsync(image.Key, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenImageNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Image);

		ProductDeletedApplicationEvent ie = new(
			Id: default,
			ImageId: ValidId,
			CadId: default
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(
			// Act
			async () => await handler.Handle(ie)
		);
	}
}
