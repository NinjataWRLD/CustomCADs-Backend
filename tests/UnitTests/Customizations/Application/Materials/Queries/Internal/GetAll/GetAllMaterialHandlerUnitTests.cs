using CustomCADs.Customizations.Application.Materials.Dtos;
using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetAll;
using CustomCADs.Customizations.Domain.Materials;
using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Customizations.Application.Materials.Queries.GetAll;

public class GetAllMaterialHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly GetAllMaterialsHandler handler;
	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private static readonly ICollection<Material> materials = [
		CreateMaterial(),
		CreateMaterial(),
		CreateMaterial(),
	];

	public GetAllMaterialHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Material>>>>()))
			.ReturnsAsync(materials);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAllMaterialsQuery query = new();

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Material>>>>()), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllMaterialsQuery query = new();

		// Act
		ICollection<MaterialDto> response = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(materials.Count, response.Count);
	}
}
