using CustomCADs.Files.Application.Cads.Commands.Shared.SetVolume;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetVolume;

using static CadsData;

public class SetCadVolumeHandlerUnitTests : CadsBaseUnitTests
{
	private readonly SetCadVolumeHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public SetCadVolumeHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateCad());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetCadVolumeCommand command = new(ValidId, ValidVolume);

		// Act
		await handler.Handle(command);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		SetCadVolumeCommand command = new(ValidId, ValidVolume);

		// Act
		await handler.Handle(command);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Cad);
		SetCadVolumeCommand command = new(ValidId, ValidVolume);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.Handle(command)
		);
	}
}
