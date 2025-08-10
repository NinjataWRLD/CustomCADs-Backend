using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.UseCases.Printing.Queries;

namespace CustomCADs.Printing.Application.Customizations.Queries.Shared.Weight;

public class GetCustomizationsWeightByIdsHandler(ICustomizationReads customizationReads, IMaterialReads materialReads, IPrintCalculator calculator)
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
				Customization customization = x.Value;
				Material material = materials[x.Value.MaterialId];

				decimal weight = calculator.CalculateWeight(customization, material);
				return Convert.ToDouble(weight);
			}
		);
	}
}
