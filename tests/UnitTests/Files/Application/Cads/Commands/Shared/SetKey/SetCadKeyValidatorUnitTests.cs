using CustomCADs.Files.Application.Cads.Commands.Shared.SetKey;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetKey;

using static CadsData;

public class SetCadKeyValidatorUnitTests : CadsBaseUnitTests
{
	private readonly SetCadKeyHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Cad cad = CreateCad();

	public SetCadKeyValidatorUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetCadKeyCommand command = new(id, ValidKey);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase_WhenCadFound()
	{
		// Arrange
		SetCadKeyCommand command = new(id, ValidKey);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldModifyCad_WhenCadFound()
	{
		// Arrange
		SetCadKeyCommand command = new(id, ValidKey);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidKey, cad.Key);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Cad);
		SetCadKeyCommand command = new(id, ValidKey);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
