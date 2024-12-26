using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Files.Application.Images.SharedCommandHandlers.SetKey;

public sealed class SetImageKeyHandler(IImageReads reads, IUnitOfWork uow)
    : ICommandHandler<SetImageKeyCommand>
{
    public async Task Handle(SetImageKeyCommand req, CancellationToken ct = default)
    {
        Image image = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ImageNotFoundException.ById(req.Id);

        image.SetKey(req.Key);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
