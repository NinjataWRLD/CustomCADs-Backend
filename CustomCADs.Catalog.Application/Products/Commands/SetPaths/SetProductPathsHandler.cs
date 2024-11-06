using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Catalog.Domain.Shared;

namespace CustomCADs.Catalog.Application.Products.Commands.SetPaths;

public class SetProductPathsHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductPathsCommand>
{
    public async Task Handle(SetProductPathsCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        if (!string.IsNullOrEmpty(req.CadPath))
        {
            product.Cad = product.Cad with { Path = req.CadPath };
        }

        if (!string.IsNullOrEmpty(req.ImagePath))
        {
            product.ImagePath = req.ImagePath;
        }

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
