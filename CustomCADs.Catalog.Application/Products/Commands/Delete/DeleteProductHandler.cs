using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Catalog.Domain.Shared;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public class DeleteProductHandler(IProductReads productReads, IWrites<Product> productWrites, IUnitOfWork uow)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand req, CancellationToken ct)
    {
        Product product = await productReads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        // Delete all Orders related to the Product here

        productWrites.Remove(product);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
