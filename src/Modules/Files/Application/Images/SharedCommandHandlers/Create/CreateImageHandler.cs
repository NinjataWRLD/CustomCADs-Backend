using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Files.Application.Images.SharedCommandHandlers.Create;

public sealed class CreateImageHandler(IWrites<Image> writes, IUnitOfWork uow)
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
