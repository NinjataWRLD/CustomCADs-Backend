using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Customizations.Commands.Internal.Edit;

public class EditCustomizationHandler(ICustomizationReads reads, IUnitOfWork uow)
    : ICommandHandler<EditCustomizationCommand>
{
    public async Task Handle(EditCustomizationCommand req, CancellationToken ct)
    {
        Customization customization = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Customization>.ById(req.Id);

        customization.SetScale(req.Scale);
        customization.SetInfill(req.Infill);
        customization.SetVolume(req.Volume);
        customization.SetColor(req.Color);
        customization.SetMaterialId(req.MaterialId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
