using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Customizations.Queries.GetById;

public class GetCustomizationByIdHandler(ICustomizationReads customizationReads, IMaterialReads materialReads)
    : IQueryHandler<GetCustomizationByIdQuery, CustomizationDto>
{
    public async Task<CustomizationDto> Handle(GetCustomizationByIdQuery req, CancellationToken ct)
    {
        Customization customization = await customizationReads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomizationNotFoundException.ById(req.Id);

        Material material = await materialReads.SingleByIdAsync(customization.MaterialId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw MaterialNotFoundException.ById(customization.MaterialId);

        return customization.ToDto(
            (Density: material.Density, Cost: material.Cost)
        );
    }
}
