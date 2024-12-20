using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public sealed class CreateProductHandler(IWrites<Product> productWrites, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateProductCommand, ProductId>
{
    public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
    {
        CreateCadCommand cadCommand = new(
            Key: req.CadKey,
            ContentType: req.CadContentType
        );
        CadId cadId = await sender.SendCommandAsync(cadCommand, ct).ConfigureAwait(false);

        CreateImageCommand imageCommand = new(
            Key: req.CadKey,
            ContentType: req.CadContentType
        );
        ImageId imageId = await sender.SendCommandAsync(imageCommand, ct).ConfigureAwait(false);

        var product = Product.Create(
            name: req.Name,
            description: req.Description,
            price: req.Price,
            imageId: imageId,
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
