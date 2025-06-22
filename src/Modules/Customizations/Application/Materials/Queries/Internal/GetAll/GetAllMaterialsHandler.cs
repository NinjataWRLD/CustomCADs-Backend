using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetAll;

public class GetAllMaterialsHandler(IMaterialReads reads, BaseCachingService<MaterialId, Material> cache)
	: IQueryHandler<GetAllMaterialsQuery, ICollection<MaterialDto>>
{
	public async Task<ICollection<MaterialDto>> Handle(GetAllMaterialsQuery req, CancellationToken ct)
	{
		ICollection<Material> materials = await cache.GetOrCreateAsync(
			factory: async () => await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false)
		).ConfigureAwait(false);

		return [.. materials.Select(x => x.ToDto())];
	}
}
