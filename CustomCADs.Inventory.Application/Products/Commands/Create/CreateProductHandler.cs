﻿using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Inventory.Application.Products.Commands.Create;

public class CreateProductHandler(IWrites<Product> productWrites, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateProductCommand, ProductId>
{
    public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
    {
        CreateCadCommand cadCommand = new(req.CadKey, req.CadContentType);
        CadId cadId = await sender.SendCommandAsync(cadCommand, ct).ConfigureAwait(false);

        var product = Product.Create(
            name: req.Name,
            description: req.Description,
            price: req.Price,
            imageKey: req.ImageKey,
            imageContentType: req.ImageContentType,
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
