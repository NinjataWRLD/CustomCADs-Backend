using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
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

        string cadPath = req.CadPath ?? product.Cad.Path;
        string imagePath = req.ImagePath ?? product.ImagePath;

        product.Cad = product.Cad with { Path = cadPath };
        product.ImagePath = imagePath;

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
