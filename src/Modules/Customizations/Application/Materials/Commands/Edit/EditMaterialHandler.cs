using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Materials.Commands.Edit;

public class EditMaterialHandler(IMaterialReads reads, IUnitOfWork uow)
    : ICommandHandler<EditMaterialCommand>
{
    public async Task Handle(EditMaterialCommand req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Material>.ById(req.Id);

        material.SetName(req.Name);
        material.SetDensity(req.Density);
        material.SetCost(req.Cost);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
