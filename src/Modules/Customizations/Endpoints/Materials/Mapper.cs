using CustomCADs.Customizations.Endpoints.Materials.Dtos;

namespace CustomCADs.Customizations.Endpoints.Materials;

internal static class Mapper
{
    internal static MaterialResponse ToResponse(this MaterialDto material)
        => new(
            Id: material.Id.Value,
            Name: material.Name,
            Density: material.Density,
            Cost: material.Cost
        );
}
