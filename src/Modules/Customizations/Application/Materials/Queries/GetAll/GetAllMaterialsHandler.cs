using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Materials.Queries.GetAll;

public class GetAllMaterialsHandler(IMaterialReads reads)
    : IQueryHandler<GetAllMaterialsQuery, ICollection<MaterialDto>>
{
    public async Task<ICollection<MaterialDto>> Handle(GetAllMaterialsQuery req, CancellationToken ct)
    {
        ICollection<Material> material = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        return [.. material.Select(x => x.ToDto())];
    }
}
