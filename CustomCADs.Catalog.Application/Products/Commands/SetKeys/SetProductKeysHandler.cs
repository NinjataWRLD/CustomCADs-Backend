using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.SetKeys;

public sealed class SetProductKeysHandler(IProductReads reads, IRequestSender sender)
    : ICommandHandler<SetProductKeysCommand>
{
    public async Task Handle(SetProductKeysCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        if (req.ImageKey is not null)
        {
            SetImageKeyCommand command = new(product.ImageId, req.ImageKey);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }

        if (req.CadKey is not null)
        {
            SetCadKeyCommand command = new(product.CadId, req.CadKey);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }
    }
}
