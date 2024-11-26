using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Common.Exceptions.Products;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Inventory.Application.Products.Commands.SetKeys;

public class SetProductKeysHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<SetProductKeysCommand>
{
    public async Task Handle(SetProductKeysCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductValidationException.Custom("Cannot modify another Creator's Products.");
        }

        if (req.ImageKey is not null)
        {
            product.SetImage(req.ImageKey);
            await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        }

        if (req.CadKey is not null)
        {
            SetCadKeyCommand command = new(product.CadId, req.CadKey);
            await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
        }
    }
}
