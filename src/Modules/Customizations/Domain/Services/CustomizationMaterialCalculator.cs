using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Materials;

namespace CustomCADs.Customizations.Domain.Services;

public class CustomizationMaterialCalculator : ICustomizationMaterialCalculator
{
	private const decimal ProfitMultiplier = 200m / 100;
	private const decimal ProfitBase = 5.0m;
	private const decimal WallFactor = 0.45m;

	public decimal CalculateWeight(Customization customization, Material material)
	{
		decimal volumeCm3 = customization.Volume / 1000;
		decimal weightG = volumeCm3 * material.Density;

		return FinalizeWeight(weightG, customization.Infill);
	}

	public decimal CalculateCost(Customization customization, Material material)
	{
		decimal weightKg = CalculateWeight(customization, material) / 1000;
		decimal costEUR = weightKg * material.Cost;

		return FinalizeCost(costEUR);
	}

	private static decimal FinalizeWeight(decimal initialWeight, decimal infill)
		=> initialWeight * (WallFactor + (1 - WallFactor) * infill);

	private static decimal FinalizeCost(decimal initialCost)
		=> initialCost * ProfitMultiplier + ProfitBase;
}
