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
            await sender.SendCommandAsync(
                new SetImageKeyCommand(product.ImageId, req.Image.Key),
                ct
            ).ConfigureAwait(false);
        }
        if (req.Image.ContentType is not null)
        {
            await sender.SendCommandAsync(
                new SetImageContentTypeCommand(product.ImageId, req.Image.ContentType),
                ct
            ).ConfigureAwait(false);
        }

        if (req.Cad.Key is not null)
        {
            await sender.SendCommandAsync(
                new SetCadKeyCommand(product.CadId, req.Cad.Key),
                ct
            ).ConfigureAwait(false);
        }
        if (req.Cad.ContentType is not null)
        {
            await sender.SendCommandAsync(
                new SetCadContentTypeCommand(product.CadId, req.Cad.ContentType),
                ct
            ).ConfigureAwait(false);
        }
        if (req.Cad.Volume is not null)
        {
            await sender.SendCommandAsync(
                new SetCadVolumeCommand(product.CadId, req.Cad.Volume.Value),
                ct
            ).ConfigureAwait(false);
        }
    }
}
