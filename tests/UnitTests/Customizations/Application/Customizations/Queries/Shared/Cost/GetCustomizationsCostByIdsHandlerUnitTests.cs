using CustomCADs.Customizations.Application.Customizations.Queries.Shared.Cost;
using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Materials;
using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Customizations.Domain.Services;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Customizations.Application.Customizations.Queries.Shared.Cost;

using static CustomizationsData;

public class GetCustomizationsCostByIdsHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationsCostByIdsHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<ICustomizationMaterialCalculator> calculator = new();

	private const decimal Cost = 10.5m;
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

	public GetCustomizationsCostByIdsHandlerUnitTests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		reads.Setup(x => x.AllByIdsAsync(ids, false, ct))
			.ReturnsAsync(customizations);

		materialReads.Setup(x => x.AllByIdsAsync(materialIds, false, ct))
			.ReturnsAsync(materials);

		calculator.Setup(x => x.CalculateCost(It.IsAny<Customization>(), It.IsAny<Material>()))
			.Returns(Cost);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCustomizationsCostByIdsQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.AllByIdsAsync(ids, false, ct), Times.Once());
		materialReads.Verify(v => v.AllByIdsAsync(materialIds, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCalculateCost()
	{
		// Arrange
		GetCustomizationsCostByIdsQuery query = new(ids);

		// Act
		await handler.Handle(query, ct);

		// Assert
		calculator.Verify(v => v.CalculateCost(
			It.Is<Customization>(x => customizations.Values.Contains(x)),
			It.Is<Material>(x => materials.Values.Contains(x))
		), Times.Exactly(customizations.Count));
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomizationsCostByIdsQuery query = new(ids);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple([.. result.Select(
			x => (Action)(() => Assert.Equal(Cost, x.Value))
		)]);
	}
}
