using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Common;
using CustomCADs.Customizations.Domain.Customizations.Reads;

namespace CustomCADs.Customizations.Application.Customizations.Commands.Edit;

public class EditCustomizationHandler(ICustomizationReads reads, IUnitOfWork uow)
    : ICommandHandler<EditCustomizationCommand>
{
    public async Task Handle(EditCustomizationCommand req, CancellationToken ct)
    {
        Customization customization = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomizationNotFoundException.ById(req.Id);

        customization.SetScale(req.Scale);
        customization.SetInfill(req.Infill);
        customization.SetVolume(req.Volume);
        customization.SetColor(req.Color);
        customization.SetMaterialId(req.MaterialId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
