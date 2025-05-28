using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.Queries.Shared;

public class GetCustomizationsWeightByIdsHandler(ICustomizationReads customizationReads, IMaterialReads materialReads)
	: IQueryHandler<GetCustomizationsWeightByIdsQuery, Dictionary<CustomizationId, double>>
{
	public async Task<Dictionary<CustomizationId, double>> Handle(GetCustomizationsWeightByIdsQuery req, CancellationToken ct)
	{
		Dictionary<CustomizationId, Customization> customizations = await customizationReads.AllByIdsAsync(req.Ids, track: false, ct).ConfigureAwait(false);
		MaterialId[] materialIds = [.. customizations.Values.Select(x => x.MaterialId).Distinct()];

		Dictionary<MaterialId, Material> materials = await materialReads.AllByIdsAsync(materialIds, track: false, ct).ConfigureAwait(false);
		return customizations.ToDictionary(
			x => x.Key,
			x =>
			{
				Material material = materials[x.Value.MaterialId];
				decimal weight = x.Value.CalculateWeight(material.Density);
				return Convert.ToDouble(weight);
			}
		);
	}
}
