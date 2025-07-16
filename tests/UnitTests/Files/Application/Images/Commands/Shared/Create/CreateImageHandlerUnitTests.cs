using CustomCADs.Files.Application.Images.Commands.Shared.Create;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Files.Application.Images.Commands.Shared.Create;

using static ImagesData;

public class CreateImageHandlerUnitTests : ImagesBaseUnitTests
{
	private readonly CreateImageHandler handler;
	private readonly Mock<IWrites<Image>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateImageHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Image>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
			ct
		)).ReturnsAsync(CreateImageWithId(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateImageCommand command = new(
			Key: ValidKey,
			ContentType: ValidContentType
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Image>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateImageCommand command = new(
			Key: ValidKey,
			ContentType: ValidContentType
		);

		// Act
		ImageId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
