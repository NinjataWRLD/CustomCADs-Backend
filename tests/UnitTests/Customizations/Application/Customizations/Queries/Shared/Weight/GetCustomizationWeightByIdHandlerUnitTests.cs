using CustomCADs.Customizations.Application.Customizations.Queries.Shared.Weight;
using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Materials;
using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Customizations.Domain.Services;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Customizations.Application.Customizations.Queries.Shared.Weight;

using static CustomizationsData;

public class GetCustomizationWeightByIdHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationWeightByIdHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<ICustomizationMaterialCalculator> calculator = new();

	private readonly Customization customization = CreateCustomization();
	private readonly Material material = CreateMaterial();

	public GetCustomizationWeightByIdHandlerUnitTests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(customization);

		materialReads.Setup(v => v.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(material);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCustomizationWeightByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, false, ct), Times.Once());
		materialReads.Verify(v => v.SingleByIdAsync(ValidMaterialId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCalculateWeight()
	{
		// Arrange
		GetCustomizationWeightByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		calculator.Verify(v => v.CalculateWeight(customization, material), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Customization);

		GetCustomizationWeightByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Customization>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenMaterialNotFound()
	{
		// Arrange
		materialReads.Setup(v => v.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(null as Material);

		GetCustomizationWeightByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Material>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
