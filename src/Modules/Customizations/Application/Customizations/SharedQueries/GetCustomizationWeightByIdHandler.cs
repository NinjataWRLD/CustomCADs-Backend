using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Customizations.Reads;
using CustomCADs.Customizations.Domain.Materials.Reads;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.SharedQueries;

public class GetCustomizationWeightByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads)
    : IQueryHandler<GetCustomizationWeightByIdQuery, double>
{
    public async Task<double> Handle(GetCustomizationWeightByIdQuery req, CancellationToken ct)
    {
        Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomizationNotFoundException.ById(req.Id);

        Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw MaterialNotFoundException.ById(customization.MaterialId);

        return Convert.ToDouble(customization.CalculateWeight(material.Density));
    }
}
