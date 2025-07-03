using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Customizations.Domain.Services;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.Queries.Shared;

public class GetCustomizationCostByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads, ICustomizationMaterialCalculator calculator)
	: IQueryHandler<GetCustomizationCostByIdQuery, decimal>
{
	public async Task<decimal> Handle(GetCustomizationCostByIdQuery req, CancellationToken ct)
	{
		Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(customization.MaterialId);

		return calculator.CalculateCost(customization, material);
	}
}
