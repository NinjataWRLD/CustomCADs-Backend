using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetFiles;

public sealed class SetProductFilesHandler(IProductReads reads, IRequestSender sender)
    : ICommandHandler<SetProductFilesCommand>
{
    public async Task Handle(SetProductFilesCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        if (req.Image.Key is not null)
        {
            SetImageKeyCommand command = new(product.ImageId, req.Image.Key);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }
        if (req.Image.ContentType is not null)
        {
            SetImageContentTypeCommand command = new(product.ImageId, req.Image.ContentType);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }

        if (req.Cad.Key is not null)
        {
            SetCadKeyCommand command = new(product.CadId, req.Cad.Key);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }
        if (req.Cad.ContentType is not null)
        {
            SetCadContentTypeCommand command = new(product.CadId, req.Cad.ContentType);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }
        if (req.Cad.Volume is not null)
        {
            SetCadVolumeCommand command = new(product.CadId, req.Cad.Volume.Value);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }
    }
}
