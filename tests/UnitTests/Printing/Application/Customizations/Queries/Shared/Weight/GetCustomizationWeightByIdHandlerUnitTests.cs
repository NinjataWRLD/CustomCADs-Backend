using CustomCADs.Printing.Application.Customizations.Queries.Shared.Weight;
using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Weight;

using static CustomizationsData;

public class GetCustomizationWeightByIdHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationWeightByIdHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<IPrintCalculator> calculator = new();

	private const double Weight = 10.5;
	private readonly Customization customization = CreateCustomization();
	private readonly Material material = CreateMaterial();

	public GetCustomizationWeightByIdHandlerUnitTests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(customization);

		materialReads.Setup(v => v.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(material);

		calculator.Setup(x => x.CalculateWeight(It.IsAny<Customization>(), It.IsAny<Material>()))
			.Returns((decimal)Weight);
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
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomizationWeightByIdQuery query = new(ValidId);

		// Act
		double result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(Weight, result);
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
