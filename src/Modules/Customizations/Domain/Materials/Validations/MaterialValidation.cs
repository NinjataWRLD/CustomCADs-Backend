using CustomCADs.Customizations.Domain.Common.Exceptions.Materials;

namespace CustomCADs.Customizations.Domain.Materials.Validations;

using static MaterialConstants;

public static class MaterialValidation
{
    public static Material ValidateName(this Material material)
    {
        string property = "Name";
        string name = material.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw MaterialValidationException.NotNull(property);
        }

        if (name.Length < NameMinLength || name.Length > NameMaxLength)
        {
            throw MaterialValidationException.Length(property, NameMinLength, NameMaxLength);
        }

        return material;
    }

    public static Material ValidateDensity(this Material material)
    {
        string property = "Density";
        decimal denisty = material.Density;

        if (denisty < DensityMin || denisty > DensityMax)
        {
            throw MaterialValidationException.Range(property, DensityMin, DensityMax);
        }

        return material;
    }

    public static Material ValidateCost(this Material material)
    {
        string property = "Cost";
        decimal cost = material.Cost;

        if (cost < CostMin || cost > CostMax)
        {
            throw MaterialValidationException.Range(property, CostMin, CostMax);
        }

        return material;
    }
}
