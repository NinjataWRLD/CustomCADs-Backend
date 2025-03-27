using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.Queries.Shared;

public class GetCustomizationsCostByIdsHandler(ICustomizationReads customizationReads, IMaterialReads materialReads)
    : IQueryHandler<GetCustomizationsCostByIdsQuery, Dictionary<CustomizationId, decimal>>
{
    public async Task<Dictionary<CustomizationId, decimal>> Handle(GetCustomizationsCostByIdsQuery req, CancellationToken ct)
    {
        Dictionary<CustomizationId, Customization> customizations = await customizationReads.AllByIdsAsync(req.Ids, track: false, ct).ConfigureAwait(false);
        MaterialId[] materialIds = [.. customizations.Values.Select(x => x.MaterialId).Distinct()];

        Dictionary<MaterialId, Material> materials = await materialReads.AllByIdsAsync(materialIds, track: false, ct).ConfigureAwait(false);
        return customizations.ToDictionary(
            x => x.Key,
            x =>
            {
                Customization customization = x.Value;
                Material material = materials[customization.MaterialId];
                return customization.CalculateCost(material.Density, material.Cost);
            }
        );
    }
}
