namespace CustomCADs.Customizations.Endpoints.Customizations;

internal static class Mapper
{
    internal static CustomizationResponse ToResponse(this CustomizationDto customization)
        => new(
            Id: customization.Id.Value,
            Scale: customization.Scale,
            Infill: customization.Infill,
            Volume: customization.Volume,
            Weight: customization.Weight,
            Cost: customization.Cost,
            Color: customization.Color,
            MaterialId: customization.MaterialId.Value
        );
}
