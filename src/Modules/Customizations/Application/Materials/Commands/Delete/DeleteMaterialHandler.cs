using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Common;
using CustomCADs.Customizations.Domain.Materials.Reads;

namespace CustomCADs.Customizations.Application.Materials.Commands.Delete;

public class DeleteMaterialHandler(IMaterialReads reads, IWrites<Material> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteMaterialCommand>
{
    public async Task Handle(DeleteMaterialCommand req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw MaterialNotFoundException.ById(req.Id);

        writes.Remove(material);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
