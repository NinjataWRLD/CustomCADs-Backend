using CustomCADs.Files.Application.Cads.Commands.Shared.SetContentType;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType.Data;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.SetContentType;

public class SetCadContentTypeHandlerUnitTests : CadsBaseUnitTests
{
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Cad cad = CreateCad();

	public SetCadContentTypeHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(cad);
	}

	[Theory]
	[ClassData(typeof(SetCadContentTypeValidData))]
	public async Task Handle_ShouldQueryDatabase(string contentType)
	{
		// Arrange
		SetCadContentTypeCommand command = new(id1, contentType);
		SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id1, true, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetCadContentTypeValidData))]
	public async Task Handle_ShouldPersistToDatabase_WhenCadFound(string contentType)
	{
		// Arrange
		SetCadContentTypeCommand command = new(id1, contentType);
		SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(SetCadContentTypeValidData))]
	public async Task Handle_ShouldModifyCad_WhenCadFound(string contentType)
	{
		// Arrange
		SetCadContentTypeCommand command = new(id1, contentType);
		SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(contentType, cad.ContentType);
	}

	[Theory]
	[ClassData(typeof(SetCadContentTypeValidData))]
	public async Task Handle_ShouldThrowException_WhenCadNotFound(string contentType)
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id1, true, ct))
			.ReturnsAsync(null as Cad);

		SetCadContentTypeCommand command = new(id1, contentType);
		SetCadContentTypeHandler handler = new(reads.Object, uow.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
