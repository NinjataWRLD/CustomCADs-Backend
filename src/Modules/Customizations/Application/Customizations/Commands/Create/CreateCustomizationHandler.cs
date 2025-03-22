using CustomCADs.Customizations.Domain.Repositories;

namespace CustomCADs.Customizations.Application.Customizations.Commands.Create;

public class CreateCustomizationHandler(IWrites<Customization> writes, IUnitOfWork uow)
    : ICommandHandler<CreateCustomizationCommand, CustomizationId>
{
    public async Task<CustomizationId> Handle(CreateCustomizationCommand req, CancellationToken ct)
    {
        var customization = Customization.Create(
            scale: req.Scale,
            infill: req.Infill,
            volume: req.Volume,
            color: req.Color,
            materialId: req.MaterialId
        );

        await writes.AddAsync(customization, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return customization.Id;
    }
}
