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

        Product product = new()
        {
            Name = req.Dto.Name,
            Description = req.Dto.Description,
            CategoryId = req.Dto.CategoryId,
            Cost = req.Dto.Cost,
            CreatorId = req.Dto.CreatorId,
            Status = req.Dto.Status,
            UploadDate = DateTime.UtcNow,
            Cad = new(),
            Category = null!, // so a new category doesn't get created
        };
        await productWrites.AddAsync(product, ct).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        return product.Id;
    }
}
