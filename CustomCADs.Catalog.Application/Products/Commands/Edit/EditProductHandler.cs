using CustomCADs.Catalog.Application.Products.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public class EditProductHandler(IProductReads reads, IUnitOfWork uow)
{
    public async Task Handle(EditProductCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new ProductNotFoundException(req.Id);

        product.Name = req.Dto.Name;
        product.Description = req.Dto.Description;
        product.Cost = req.Dto.Cost;
        product.CategoryId = req.Dto.CategoryId;

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
