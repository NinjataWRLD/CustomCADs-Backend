using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Printing.Application.Customizations.Queries.Shared.Cost;

public class GetCustomizationCostByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads, IPrintCalculator calculator)
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
