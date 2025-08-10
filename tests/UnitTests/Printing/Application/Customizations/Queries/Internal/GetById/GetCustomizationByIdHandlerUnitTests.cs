using CustomCADs.Printing.Application.Customizations.Queries.Internal.GetById;
using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Internal.GetById;

using static CustomizationsData;

public class GetCustomizationByIdHandlerUnitTests : CustomizationsBaseUnitTests
{
	private readonly GetCustomizationByIdHandler handler;
	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<IPrintCalculator> calculator = new();

	private readonly Customization customization = CreateCustomizationWithId(ValidId);
	private readonly Material material = CreateMaterial();

	public GetCustomizationByIdHandlerUnitTests()
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
		GetCustomizationByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(v => v.SingleByIdAsync(ValidId, false, ct), Times.Once());
		materialReads.Verify(v => v.SingleByIdAsync(ValidMaterialId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldCalculateWeightAndCost()
	{
		// Arrange
		GetCustomizationByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		calculator.Verify(v => v.CalculateWeight(customization, material), Times.Once());
		calculator.Verify(v => v.CalculateCost(customization, material), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCustomizationByIdQuery query = new(ValidId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidId, result.Id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(v => v.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Customization);

		GetCustomizationByIdQuery query = new(ValidId);

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

		GetCustomizationByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Material>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
