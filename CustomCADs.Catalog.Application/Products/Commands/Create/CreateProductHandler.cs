using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Categories;
using CustomCADs.Catalog.Domain.Products.Entities;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public class CreateProductHandler(ICategoryReads categoryReads, IWrites<Product> productWrites, IUnitOfWork uow)
    : ICommandHandler<CreateProductCommand, ProductId>
{
    public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
    {
        bool categoryExists = await categoryReads.ExistsByIdAsync(req.CategoryId, ct: ct).ConfigureAwait(false);
        if (!categoryExists)
        {
            throw CategoryNotFoundException.ById(req.CategoryId);
        }

        Product product = Product.Create(
            name: req.Name,
            description: req.Description,
            price: req.Price,
            status: req.Status,
            creatorId: req.CreatorId,
            categoryId: req.CategoryId
        );
        await productWrites.AddAsync(product, ct).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        return product.Id;
    }
}
