using CustomCADs.Printing.Application.Materials.Commands.Internal.Edit;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Commands.Internal.Edit;

using static MaterialsData;

public class EditMaterialHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly EditMaterialHandler handler;
	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private readonly Material material = CreateMaterial();

	public EditMaterialHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(material);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		EditMaterialCommand command = new(
			Id: ValidId,
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		EditMaterialCommand command = new(
			Id: ValidId,
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		EditMaterialCommand command = new(
			Id: ValidId,
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.UpdateAsync(ValidId, material), Times.Once());
	}
}
