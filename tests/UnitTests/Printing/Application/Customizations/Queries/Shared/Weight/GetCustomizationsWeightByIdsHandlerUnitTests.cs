using CustomCADs.Printing.Application.Customizations.Queries.Shared.Weight;
using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Weight;

using static CustomizationsData;

public class GetCustomizationsWeightByIdsHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationsWeightByIdsHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<IPrintCalculator> calculator = new();

	private const double Weight = 10.5;
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

		calculator.Setup(x => x.CalculateWeight(It.IsAny<Customization>(), It.IsAny<Material>()))
			.Returns((decimal)Weight);
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

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomizationsWeightByIdsQuery query = new(ids);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple([.. result.Select(
			x => (Action)(() => Assert.Equal(Weight, x.Value))
		)]);
	}
}
