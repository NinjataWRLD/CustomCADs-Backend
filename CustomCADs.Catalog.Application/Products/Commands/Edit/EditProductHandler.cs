using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public class EditProductHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<EditProductCommand>
{
    public async Task Handle(EditProductCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        product.Name = req.Dto.Name;
        product.Description = req.Dto.Description;
        product.Price = req.Dto.Price;
        product.CategoryId = req.Dto.CategoryId;

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
