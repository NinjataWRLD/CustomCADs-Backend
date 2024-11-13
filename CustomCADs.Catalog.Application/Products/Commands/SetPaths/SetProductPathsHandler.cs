using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Commands.SetPaths;

public class SetProductPathsHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<SetProductPathsCommand>
{
    public async Task Handle(SetProductPathsCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        product.SetPaths(imagePath: req.ImagePath, cadPath: req.CadPath);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
