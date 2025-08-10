using CustomCADs.Printing.Domain.Services;

namespace CustomCADs.UnitTests.Printing.Domain.Services.Calculator;

public class PrintingCalculatorUnitTests : ServicesBaseUnitTests
{
	private readonly PrintCalculator calculator = new();

	[Fact]
	public void CalculateWeight_ShouldReturnExpected()
	{
		decimal weight = calculator.CalculateWeight(
			customization: CreateCustomization(volume: 10_000, infill: 0.2m),
			material: CreateMaterial(density: 1.25m)
		);

		// Expected: (10000 / 1000) * 1.25 = 12.5g raw
		// Final weight = 12.5 * (0.45 + (1 - 0.45) * 0.2) = 12.5 * (0.45 + 0.11) = 12.5 * 0.56 = 7.0
		Assert.Equal(7m, weight);
	}

	[Fact]
	public void CalculateCost_ShouldReturnExpected()
	{
		decimal cost = calculator.CalculateCost(
			customization: CreateCustomization(volume: 10000, infill: 0.2m),
			material: CreateMaterial(density: 1.25m, cost: 20.0m)
		);

		// Expected: (7 / 1000) * 20.0 = 0.14€ raw
		// Final cost = 0.14 * 2 + 5 = 0.28 + 5 = 5.28
		Assert.Equal(5.28m, cost);
	}
}
