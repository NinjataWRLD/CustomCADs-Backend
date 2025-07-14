using CustomCADs.Files.Application.Cads.Commands.Shared.Create;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.Create;

using static CadsData;

public class CreateCadHandlerUnitTests : CadsBaseUnitTests
{
	private readonly CreateCadHandler handler;
	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateCadHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateCadCommand command = new(
			Key: ValidKey,
			ContentType: ValidContentType,
			Volume: ValidVolume
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Cad>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}
}
