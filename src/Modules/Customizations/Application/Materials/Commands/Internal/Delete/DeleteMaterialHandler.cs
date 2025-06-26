using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Materials.Commands.Internal.Delete;

public class DeleteMaterialHandler(IMaterialReads reads, IWrites<Material> writes, IUnitOfWork uow, BaseCachingService<MaterialId, Material> cache)
	: ICommandHandler<DeleteMaterialCommand>
{
	public async Task Handle(DeleteMaterialCommand req, CancellationToken ct)
	{
		Material material = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Material>.ById(req.Id)
		).ConfigureAwait(false);

		writes.Remove(material);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.ClearAsync(req.Id).ConfigureAwait(false);
	}
}
