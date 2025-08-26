using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Printing.Domain.Repositories.Reads;

namespace CustomCADs.Printing.Application.Materials.Commands.Internal.Edit;

public class EditMaterialHandler(IMaterialReads reads, BaseCachingService<MaterialId, Material> cache, IUnitOfWork uow)
	: ICommandHandler<EditMaterialCommand>
{
	public async Task Handle(EditMaterialCommand req, CancellationToken ct)
	{
		Material material = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Material>.ById(req.Id);

		material
			.SetName(req.Name)
			.SetDensity(req.Density)
			.SetCost(req.Cost);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(req.Id, material).ConfigureAwait(false);
	}
}
