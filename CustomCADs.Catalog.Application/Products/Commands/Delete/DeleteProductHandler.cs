using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products.Entities;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public class DeleteProductHandler(IProductReads productReads, IWrites<Product> productWrites, IUnitOfWork uow)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand req, CancellationToken ct)
    {
        Product product = await productReads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        // Delete all Orders related to the Product here

        productWrites.Remove(product);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
