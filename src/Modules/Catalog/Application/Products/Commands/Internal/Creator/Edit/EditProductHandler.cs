using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;

public sealed class EditProductHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<EditProductCommand>
{
    public async Task Handle(EditProductCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw CustomAuthorizationException<Product>.ById(req.Id);
        }

        if (!await sender.SendQueryAsync(new GetCategoryExistsByIdQuery(req.CategoryId), ct).ConfigureAwait(false))
            throw CustomNotFoundException<Product>.ById(req.CategoryId, "Category");

        product
            .SetName(req.Name)
            .SetDescription(req.Description)
            .SetPrice(req.Price)
            .SetCategoryId(req.CategoryId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
