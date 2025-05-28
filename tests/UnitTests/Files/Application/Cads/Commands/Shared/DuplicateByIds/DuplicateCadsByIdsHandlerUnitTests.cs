using CustomCADs.Files.Application.Cads.Commands.Shared.DuplicateByIds;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.DuplicateByIds;

using static CadsData;

public class DuplicateCadsByIdsHandlerUnitTests : CadsBaseUnitTests
{
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Cad[] cads = [
		CreateCadWithId(id1, ValidKey1, ValidContentType1, ValidVolume1, ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord1, ValidCoord1),
		CreateCadWithId(id2, ValidKey2, ValidContentType2, ValidVolume2, ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord2, ValidCoord2),
	];
	private readonly CadId[] ids = [id1, id2];
	private readonly CadQuery query;
	private readonly Result<Cad> result;

	public DuplicateCadsByIdsHandlerUnitTests()
	{
		query = new(new(1, ids.Length), ids);
		result = new Result<Cad>(cads.Length, cads);
		reads.Setup(x => x.AllAsync(query, false, ct))
			.ReturnsAsync(result);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DuplicateCadsByIdsCommand command = new(ids);
		DuplicateCadsByIdsHandler handler = new(reads.Object, writes.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.AllAsync(query, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DuplicateCadsByIdsCommand command = new(ids);
		DuplicateCadsByIdsHandler handler = new(reads.Object, writes.Object, uow.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(It.IsAny<Cad>(), ct), Times.Exactly(cads.Length));
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnKeysProperly()
	{
		// Arrange
		DuplicateCadsByIdsCommand command = new(ids);
		DuplicateCadsByIdsHandler handler = new(reads.Object, writes.Object, uow.Object);

		// Act
		var result = await handler.Handle(command, ct);

		// Assert
		var actual = result.Select(x => x.Key);
		var expected = cads.Select(x => x.Id);
		Assert.Equal(expected, actual);
	}
}
