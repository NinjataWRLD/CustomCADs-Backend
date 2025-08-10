using CustomCADs.Printing.Application.Materials.Dtos;
using CustomCADs.Printing.Application.Materials.Queries.Internal.GetById;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Queries.Internal.GetById;

using static MaterialsData;

public class GetMaterialByIdHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly GetMaterialByIdHandler handler;
	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private static readonly Material material = CreateMaterialWithId();

	public GetMaterialByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()))
			.ReturnsAsync(material);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetMaterialByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetMaterialByIdQuery query = new(ValidId);

		// Act
		MaterialDto response = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(material.Id, response.Id);
	}
}
