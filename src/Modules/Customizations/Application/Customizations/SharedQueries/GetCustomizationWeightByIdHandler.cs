using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.SharedQueries;

public class GetCustomizationWeightByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads)
    : IQueryHandler<GetCustomizationWeightByIdQuery, double>
{
    public async Task<double> Handle(GetCustomizationWeightByIdQuery req, CancellationToken ct)
    {
        Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Customization>.ById(req.Id);

        Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Material>.ById(customization.MaterialId);

        return Convert.ToDouble(customization.CalculateWeight(material.Density));
    }
}
