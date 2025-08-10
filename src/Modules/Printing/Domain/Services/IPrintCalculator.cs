using CustomCADs.Printing.Domain.Customizations;
using CustomCADs.Printing.Domain.Materials;

namespace CustomCADs.Printing.Domain.Services;

public interface IPrintCalculator
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
