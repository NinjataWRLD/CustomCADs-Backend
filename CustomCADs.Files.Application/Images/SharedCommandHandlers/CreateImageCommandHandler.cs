using CustomCADs.Files.Domain.Common;
using CustomCADs.Files.Domain.Images;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Files.Application.Images.SharedCommandHandlers;

public sealed class CreateImageCommandHandler(IWrites<Image> writes, IUnitOfWork uow)
    : ICommandHandler<CreateImageCommand, ImageId>
{
    public async Task<ImageId> Handle(CreateImageCommand req, CancellationToken ct)
    {
        Image image = Image.Create(req.Key, req.ContentType);

        await writes.AddAsync(image, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return image.Id;
    }
}
