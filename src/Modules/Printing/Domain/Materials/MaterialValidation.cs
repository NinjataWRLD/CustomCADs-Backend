namespace CustomCADs.Printing.Domain.Materials;

using static MaterialConstants;

public static class MaterialValidation
{
	public static Material ValidateName(this Material material)
	{
		string property = "Name";
		string name = material.Name;

		if (string.IsNullOrEmpty(name))
		{
			throw CustomValidationException<Material>.NotNull(property);
		}

		if (name.Length < NameMinLength || name.Length > NameMaxLength)
		{
			throw CustomValidationException<Material>.Length(property, NameMinLength, NameMaxLength);
		}

		return material;
	}

	public static Material ValidateDensity(this Material material)
	{
		string property = "Density";
		decimal denisty = material.Density;

		if (denisty < DensityMin || denisty > DensityMax)
		{
			throw CustomValidationException<Material>.Range(property, DensityMin, DensityMax);
		}

		return material;
	}

	public static Material ValidateCost(this Material material)
	{
		string property = "Cost";
		decimal cost = material.Cost;

		if (cost < CostMin || cost > CostMax)
		{
			throw CustomValidationException<Material>.Range(property, DensityMin, DensityMax);
		}

		return material;
	}
}
