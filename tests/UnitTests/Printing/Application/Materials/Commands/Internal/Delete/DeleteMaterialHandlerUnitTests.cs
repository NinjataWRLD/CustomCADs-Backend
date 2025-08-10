using CustomCADs.Printing.Application.Materials.Commands.Internal.Delete;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Commands.Internal.Delete;

using static MaterialsData;

public class DeleteMaterialHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly DeleteMaterialHandler handler;
	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<IWrites<Material>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private readonly Material material = CreateMaterial();

	public DeleteMaterialHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Material>>>()
		)).ReturnsAsync(material);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		DeleteMaterialCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Material>>>()
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteMaterialCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(material), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldClearCache()
	{
		// Arrange
		DeleteMaterialCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.ClearAsync(ValidId), Times.Once());
	}
}
