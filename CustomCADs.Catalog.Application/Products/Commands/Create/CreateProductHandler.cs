using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Shared;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public class CreateProductHandler(ICategoryReads categoryReads, IWrites<Product> productWrites, IUnitOfWork uow)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand req, CancellationToken ct)
    {
        bool categoryExists = await categoryReads.ExistsByIdAsync(req.Dto.CategoryId, ct: ct).ConfigureAwait(false);
        if (!categoryExists)
        {
            throw new CategoryNotFoundException(req.Dto.CategoryId);
        }

        Product product = Product.Create(
            name: req.Dto.Name,
            description: req.Dto.Description,
            categoryId: req.Dto.CategoryId,
            cost: req.Dto.Cost,
            status: req.Dto.Status,
            uploadDate: DateTime.UtcNow,
            imagePath: string.Empty,
            creatorId: req.Dto.CreatorId
        );
        await productWrites.AddAsync(product, ct).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        return product.Id;
    }
}
