namespace CustomCADs.Customizations.Application.Customizations;

internal static class Mapper
{
    internal static CustomizationDto ToDto(this Customization customization, (decimal Density, decimal Cost) material)
        => new(
            Id: customization.Id,
            Scale: customization.Scale,
            Infill: customization.Infill,
            Volume: customization.Volume,
            Weight: Convert.ToDouble(customization.CalculateWeight(material.Density)),
            Cost: customization.CalculateCost(material.Density, material.Cost),
            Color: customization.Color,
            MaterialId: customization.MaterialId
        );
}
