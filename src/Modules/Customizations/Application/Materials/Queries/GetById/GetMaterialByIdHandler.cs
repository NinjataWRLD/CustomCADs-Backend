using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Materials.Reads;

namespace CustomCADs.Customizations.Application.Materials.Queries.GetById;

public class GetMaterialByIdHandler(IMaterialReads reads)
    : IQueryHandler<GetMaterialByIdQuery, MaterialDto>
{
    public async Task<MaterialDto> Handle(GetMaterialByIdQuery req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw MaterialNotFoundException.ById(req.Id);

        return material.ToDto();
    }
}
