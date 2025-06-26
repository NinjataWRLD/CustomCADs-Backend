using CustomCADs.Files.Application.Images.Commands.Shared.SetContentType;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.SetContentType;

using static ImagesData;

public class SetImageContentTypeHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly SetImageContentTypeHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Image image = CreateImage();

	public SetImageContentTypeHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(image);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetImageContentTypeCommand command = new(id, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase_WhenImageFound()
	{
		// Arrange
		SetImageContentTypeCommand command = new(id, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldModifyImage_WhenImageFound()
	{
		// Arrange
		SetImageContentTypeCommand command = new(id, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidContentType, image.ContentType);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenImageNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Image);
		SetImageContentTypeCommand command = new(id, ValidContentType);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
