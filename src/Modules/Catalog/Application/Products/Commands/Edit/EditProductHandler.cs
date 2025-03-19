using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public sealed class EditProductHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<EditProductCommand>
{
    public async Task Handle(EditProductCommand req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.CreatorId != req.CreatorId)
        {
            throw ProductAuthorizationException.ByProductId(req.Id);
        }

        GetCategoryExistsByIdQuery categoryQuery = new(req.CategoryId);
        bool categoryExists = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);
        if (!categoryExists)
        {
            throw ProductNotFoundException.CategoryId(req.CategoryId);
        }

        product
            .SetName(req.Name)
            .SetDescription(req.Description)
            .SetPrice(req.Price)
            .SetCategoryId(req.CategoryId);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
