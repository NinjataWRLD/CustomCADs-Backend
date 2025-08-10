using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Printing.Domain.Services;

namespace CustomCADs.Printing.Application.Customizations.Queries.Internal.GetById;

public class GetCustomizationByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads, IPrintCalculator calculator)
	: IQueryHandler<GetCustomizationByIdQuery, CustomizationDto>
{
	public async Task<CustomizationDto> Handle(GetCustomizationByIdQuery req, CancellationToken ct)
	{
		Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(customization.MaterialId);

		return customization.ToDto(
			weight: calculator.CalculateWeight(customization, material),
			cost: calculator.CalculateCost(customization, material)
		);
	}
}
