using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Entities;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public class CreateProductHandler(ICategoryReads categoryReads, IWrites<Product> productWrites, IUnitOfWork uow)
    : ICommandHandler<CreateProductCommand, ProductId>
{
    public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
    {
        bool categoryExists = await categoryReads.ExistsByIdAsync(req.Dto.CategoryId, ct: ct).ConfigureAwait(false);
        if (!categoryExists)
        {
            throw CategoryNotFoundException.ById(req.Dto.CategoryId);
        }

        Product product = Product.Create(
            name: req.Dto.Name,
            description: req.Dto.Description,
            categoryId: req.Dto.CategoryId,
            price: req.Dto.Price,
            status: req.Dto.Status,
            creatorId: req.Dto.CreatorId
        );
        await productWrites.AddAsync(product, ct).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        return product.Id;
    }
}
