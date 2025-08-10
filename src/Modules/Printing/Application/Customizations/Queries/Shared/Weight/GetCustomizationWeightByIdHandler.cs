using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.Printing.Application.Customizations.Queries.Shared.Weight;

public class GetCustomizationWeightByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads, IPrintCalculator calculator)
	: IQueryHandler<GetCustomizationWeightByIdQuery, double>
{
	public async Task<double> Handle(GetCustomizationWeightByIdQuery req, CancellationToken ct)
	{
		Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(customization.MaterialId);

		return Convert.ToDouble(calculator.CalculateWeight(customization, material));
	}
}
