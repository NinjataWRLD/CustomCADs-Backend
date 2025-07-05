using CustomCADs.Customizations.Application.Customizations.Queries.Shared.Weight;
using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Materials;
using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Customizations.Domain.Services;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Customizations.Application.Customizations.Queries.Shared.Weight;

using static CustomizationsData;

public class GetCustomizationsWeightByIdsHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationsWeightByIdsHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<ICustomizationMaterialCalculator> calculator = new();

	private static readonly CustomizationId[] ids = [];
	private static readonly Dictionary<CustomizationId, Customization> customizations = new()
	{
		[ValidId] = CreateCustomization(),
	};
	private static readonly MaterialId[] materialIds = [.. customizations.Values.Select(x => x.MaterialId)];
	private static readonly Dictionary<MaterialId, Material> materials = new()
	{
		[ValidMaterialId] = CreateMaterial(),
	};

	public GetCustomizationsWeightByIdsHandlerUnitTests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		reads.Setup(v => v.AllByIdsAsync(ids, false, ct))
			.ReturnsAsync(customizations);

		materialReads.Setup(v => v.AllByIdsAsync(materialIds, false, ct))
			.ReturnsAsync(materials);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCustomizationsWeightByIdsQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.AllByIdsAsync(ids, false, ct), Times.Once());
		materialReads.Verify(v => v.AllByIdsAsync(materialIds, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCalculateWeight()
	{
		// Arrange
		GetCustomizationsWeightByIdsQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		calculator.Verify(v => v.CalculateWeight(
			It.Is<Customization>(x => customizations.Values.Contains(x)),
			It.Is<Material>(x => materials.Values.Contains(x))
		), Times.Exactly(customizations.Count));
	}
}
