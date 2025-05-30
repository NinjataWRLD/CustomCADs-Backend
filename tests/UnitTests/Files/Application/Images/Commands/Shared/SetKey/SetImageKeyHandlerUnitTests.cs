using CustomCADs.Files.Application.Images.Commands.Shared.SetKey;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.SetKey;

using Data;

public class SetImageKeyHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly SetImageKeyHandler handler;
	private readonly Mock<IImageReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Image image = CreateImage();

	public SetImageKeyHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(image);
	}

	[Theory]
	[ClassData(typeof(SetImageKeyValidData))]
	public async Task Handle_ShouldQueryDatabase(string key)
	{
		// Arrange
		SetImageKeyCommand command = new(id1, key);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetImageKeyValidData))]
	public async Task Handle_ShouldPersistToDatabase_WhenImageFound(string key)
	{
		// Arrange
		SetImageKeyCommand command = new(id1, key);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetImageKeyValidData))]
	public async Task Handle_ShouldModifyImage_WhenImageFound(string key)
	{
		// Arrange
		SetImageKeyCommand command = new(id1, key);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(key, image.Key);
	}

	[Theory]
	[ClassData(typeof(SetImageKeyValidData))]
	public async Task Handle_ShouldThrowException_WhenImageNotFound(string key)
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(null as Image);
		SetImageKeyCommand command = new(id1, key);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Image>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
