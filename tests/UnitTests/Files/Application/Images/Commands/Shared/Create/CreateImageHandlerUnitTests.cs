using CustomCADs.Files.Application.Images.Commands.Shared.Create;
using CustomCADs.Files.Domain.Repositories;
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
}
