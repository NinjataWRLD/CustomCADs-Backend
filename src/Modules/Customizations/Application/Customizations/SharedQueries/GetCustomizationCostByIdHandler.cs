using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Customizations.Application.Customizations.SharedQueries;

public class GetCustomizationCostByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads)
    : IQueryHandler<GetCustomizationCostByIdQuery, decimal>
{
    public async Task<decimal> Handle(GetCustomizationCostByIdQuery req, CancellationToken ct)
    {
        Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomizationNotFoundException.ById(req.Id);

        Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw MaterialNotFoundException.ById(customization.MaterialId);

        return customization.CalculateCost(material.Density, material.Cost);
    }
}
