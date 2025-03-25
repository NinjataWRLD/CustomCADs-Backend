using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Customizations.Domain.Repositories.Reads;

namespace CustomCADs.Customizations.Application.Customizations.Commands.Delete;

public class EditCustomizationHandler(ICustomizationReads reads, IWrites<Customization> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteCustomizationCommand>
{
    public async Task Handle(DeleteCustomizationCommand req, CancellationToken ct)
    {
        Customization customization = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Customization>.ById(req.Id);

        writes.Remove(customization);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
