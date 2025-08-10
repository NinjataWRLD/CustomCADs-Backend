using CustomCADs.Printing.Application.Customizations.Queries.Shared.Cost;
using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Cost;

using static CustomizationsData;

public class GetCustomizationCostByIdHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationCostByIdHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<IPrintCalculator> calculator = new();

	private const decimal Cost = 10.5m;
	private readonly Customization customization = CreateCustomization();
	private readonly Material material = CreateMaterial();

	public GetCustomizationCostByIdHandlerUnitTests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		calculator.Setup(x => x.CalculateCost(customization, material))
			.Returns(Cost);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(customization);

		materialReads.Setup(x => x.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(material);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetCustomizationCostByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, false, ct), Times.Once());
		materialReads.Verify(v => v.SingleByIdAsync(ValidMaterialId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCalculateCost()
	{
		// Arrange
		GetCustomizationCostByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		calculator.Verify(v => v.CalculateCost(customization, material), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomizationCostByIdQuery query = new(ValidId);

		// Act
		decimal result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(Cost, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Customization);

		GetCustomizationCostByIdQuery query = new(ValidId);

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

		GetCustomizationCostByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Material>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
