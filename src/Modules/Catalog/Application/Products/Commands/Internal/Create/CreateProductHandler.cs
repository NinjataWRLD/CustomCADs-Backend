using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Categories.Queries;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Create;

using static Constants.Roles;

public sealed class CreateProductHandler(IProductWrites productWrites, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateProductCommand, ProductId>
{
    public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
    {
        GetCategoryExistsByIdQuery categoryQuery = new(req.CategoryId);
        bool categoryExists = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);
        if (!categoryExists)
            throw CustomNotFoundException<Product>.ById(req.CategoryId, "Category");

        GetAccountExistsByIdQuery creatorQuery = new(req.CreatorId);
        bool creatorExists = await sender.SendQueryAsync(creatorQuery, ct).ConfigureAwait(false);
        if (!creatorExists)
            throw CustomNotFoundException<Product>.ById(req.CreatorId, "User");

        CreateCadCommand cadCommand = new(
            Key: req.CadKey,
            ContentType: req.CadContentType,
            Volume: req.CadVolume
        );
        CadId cadId = await sender.SendCommandAsync(cadCommand, ct).ConfigureAwait(false);

        CreateImageCommand imageCommand = new(
            Key: req.ImageKey,
            ContentType: req.ImageContentType
        );
        ImageId imageId = await sender.SendCommandAsync(imageCommand, ct).ConfigureAwait(false);

        var product = Product.Create(
            name: req.Name,
            description: req.Description,
            price: req.Price,
            categoryId: req.CategoryId,
            creatorId: req.CreatorId,
            imageId: imageId,
            cadId: cadId
        );

        GetUserRoleByIdQuery roleQuery = new(req.CreatorId);
        string role = await sender.SendQueryAsync(roleQuery, ct).ConfigureAwait(false);

        if (role is Designer)
            product.SetValidatedStatus();

        await productWrites.AddAsync(product, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return product.Id;
    }
}
