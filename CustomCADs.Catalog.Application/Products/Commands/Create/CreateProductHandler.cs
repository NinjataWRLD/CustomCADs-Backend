using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Common.Exceptions.Categories;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Queries.Cads;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public class CreateProductHandler(ICategoryReads categoryReads, IWrites<Product> productWrites, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateProductCommand, ProductId>
{
    public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
    {
        bool categoryExists = await categoryReads.ExistsByIdAsync(req.CategoryId, ct: ct).ConfigureAwait(false);
        if (!categoryExists)
        {
            throw CategoryNotFoundException.ById(req.CategoryId);
        }

        GetCadIdByPathQuery cadQuery = new(req.CadPath);
        CadId cadId = await sender.SendQueryAsync(cadQuery, ct).ConfigureAwait(false);

        var product = Product.Create(
            name: req.Name,
            description: req.Description,
            price: req.Price,
            imagePath: req.ImagePath,
            status: req.Status,
            creatorId: req.CreatorId,
            categoryId: req.CategoryId,
            cadId: cadId
        );
        await productWrites.AddAsync(product, ct).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        return product.Id;
    }
}
