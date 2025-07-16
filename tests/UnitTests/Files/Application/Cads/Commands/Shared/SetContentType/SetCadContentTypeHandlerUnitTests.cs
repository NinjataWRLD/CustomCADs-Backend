using CustomCADs.Files.Application.Cads.Commands.Shared.SetContentType;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType;

using static CadsData;

public class SetCadContentTypeHandlerUnitTests : CadsBaseUnitTests
{
	private readonly SetCadContentTypeHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Cad cad = CreateCad();

	public SetCadContentTypeHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetCadContentTypeCommand command = new(ValidId, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		SetCadContentTypeCommand command = new(ValidId, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldModifyCad()
	{
		// Arrange
		SetCadContentTypeCommand command = new(ValidId, ValidContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidContentType, cad.ContentType);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Cad);
		SetCadContentTypeCommand command = new(ValidId, ValidContentType);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
