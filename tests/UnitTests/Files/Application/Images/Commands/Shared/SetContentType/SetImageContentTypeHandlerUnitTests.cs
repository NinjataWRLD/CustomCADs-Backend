using CustomCADs.Files.Application.Images.Commands.Shared.SetContentType;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.SetContentType;

using Data;

public class SetImageContentTypeHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly SetImageContentTypeHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Image image = CreateImage();

	public SetImageContentTypeHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(image);
	}

	[Theory]
	[ClassData(typeof(SetImageContentTypeValidData))]
	public async Task Handle_ShouldQueryDatabase(string contentType)
	{
		// Arrange
		SetImageContentTypeCommand command = new(id1, contentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetImageContentTypeValidData))]
	public async Task Handle_ShouldPersistToDatabase_WhenImageFound(string contentType)
	{
		// Arrange
		SetImageContentTypeCommand command = new(id1, contentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetImageContentTypeValidData))]
	public async Task Handle_ShouldModifyImage_WhenImageFound(string contentType)
	{
		// Arrange
		SetImageContentTypeCommand command = new(id1, contentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(contentType, image.ContentType);
	}

	[Theory]
	[ClassData(typeof(SetImageContentTypeValidData))]
	public async Task Handle_ShouldThrowException_WhenImageNotFound(string contentType)
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(null as Image);
		SetImageContentTypeCommand command = new(id1, contentType);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
