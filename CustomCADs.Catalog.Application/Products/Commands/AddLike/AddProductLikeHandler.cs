using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Reads;

namespace CustomCADs.Catalog.Application.Products.Commands.AddLike;

public sealed class AddProductLikeHandler(IProductReads reads, IUnitOfWork uow)
    : ICommandHandler<AddProductLikeCommand>
{
    public async Task Handle(AddProductLikeCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        product.AddToLikeCount();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
