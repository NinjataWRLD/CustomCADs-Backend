using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Materials.Commands.Internal.Edit;

public class EditMaterialHandler(IMaterialReads reads, BaseCachingService<MaterialId, Material> cache, IUnitOfWork uow)
	: ICommandHandler<EditMaterialCommand>
{
	public async Task Handle(EditMaterialCommand req, CancellationToken ct)
	{
		Material material = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Material>.ById(req.Id)
		).ConfigureAwait(false);

		material.SetName(req.Name);
		material.SetDensity(req.Density);
		material.SetCost(req.Cost);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(req.Id, material).ConfigureAwait(false);
	}
}
