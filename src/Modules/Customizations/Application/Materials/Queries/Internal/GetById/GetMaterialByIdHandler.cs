using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetById;

public class GetMaterialByIdHandler(IMaterialReads reads)
	: IQueryHandler<GetMaterialByIdQuery, MaterialDto>
{
	public async Task<MaterialDto> Handle(GetMaterialByIdQuery req, CancellationToken ct)
	{
		Material material = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(req.Id);

		return material.ToDto();
	}
}
