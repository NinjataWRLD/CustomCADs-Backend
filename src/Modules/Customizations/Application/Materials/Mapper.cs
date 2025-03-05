namespace CustomCADs.Customizations.Application.Materials;

internal static class Mapper
{
    public static MaterialDto ToDto(this Material material)
        => new(
            Id: material.Id,
            Name: material.Name,
            Density: material.Density,
            Cost: material.Cost
        );
}
