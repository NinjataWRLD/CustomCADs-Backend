using CustomCADs.Files.Application.Images.Commands.Shared.SetContentType;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.SetContentType;

using static ImagesData;

public class SetImageContentTypeHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly SetImageContentTypeHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<ImageId, Image>> cache = new();

	private readonly Image image = CreateImageWithId();

	public SetImageContentTypeHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(image);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetImageContentTypeCommand command = new(ValidId, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		SetImageContentTypeCommand command = new(ValidId, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		SetImageContentTypeCommand command = new(ValidId, ValidKey);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, image),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldModifyImage()
	{
		// Arrange
		SetImageContentTypeCommand command = new(ValidId, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidContentType, image.ContentType);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenImageNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Image);
		SetImageContentTypeCommand command = new(ValidId, ValidContentType);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
