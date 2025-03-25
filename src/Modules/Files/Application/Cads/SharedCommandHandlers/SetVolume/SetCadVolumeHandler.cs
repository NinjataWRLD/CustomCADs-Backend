using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetVolume;

public sealed class SetCadVolumeHandler(ICadReads reads, IUnitOfWork uow)
    : ICommandHandler<SetCadVolumeCommand>
{
    public async Task Handle(SetCadVolumeCommand req, CancellationToken ct = default)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Cad>.ById(req.Id);

        cad.SetVolume(req.Volume);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
