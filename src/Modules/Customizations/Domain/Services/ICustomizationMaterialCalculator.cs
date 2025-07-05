using CustomCADs.Customizations.Domain.Customizations;
using CustomCADs.Customizations.Domain.Materials;

namespace CustomCADs.Customizations.Domain.Services;

public interface ICustomizationMaterialCalculator
{
	/// <summary>
	///     Calculate Weight
	/// </summary>
	/// <param name="customization">The customization</param>
	/// <param name="material">The material</param>
	/// <returns>Weight in grams</returns>
	decimal CalculateWeight(Customization customization, Material material);

	/// <summary>
	///     Calculate Cost
	/// </summary>
	/// <param name="customization">The customization</param>
	/// <param name="material">The material</param>
	/// <returns>Cost in €</returns>
	decimal CalculateCost(Customization customization, Material material);
}
