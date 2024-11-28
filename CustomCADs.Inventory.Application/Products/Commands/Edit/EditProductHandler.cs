using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Common.Exceptions.Products;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;

namespace CustomCADs.Inventory.Application.Products.Commands.Edit;

public class EditProductHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<EditProductCommand>
{
    public async Task Handle(EditProductCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductValidationException.Unauthorized();
        }

        product
            .SetName(req.Name)
            .SetDescription(req.Description)
            .SetPrice(req.Price)
            .SetCategoryId(req.CategoryId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
