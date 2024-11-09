﻿using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

using static ApiMessages;

public class PutProductEndpoint(IRequestSender sender, IEventRaiser raiser) 
    : Endpoint<PutProductRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PutProductRequest>("multipart/form-data"));
    }

    public override async Task HandleAsync(PutProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        EditProductDto dto = new(
            Name: req.Name,
            Description: req.Description,
            CategoryId: req.CategoryId,
            Cost: req.Cost
        );
        EditProductCommand command = new(req.Id, dto);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetProductByIdQuery getProductQuery = new(req.Id);
        GetProductByIdDto product = await sender.SendQueryAsync(getProductQuery, ct).ConfigureAwait(false);

        ProductEditedDomainEvent productEditedDomainEvent = new(
            Id: product.Id,
            OldName: product.Name,
            Name: dto.Name,
            OldDescription: product.Description,
            Description: dto.Description,
            OldCategoryId: product.Category.Id,
            CategoryId: dto.CategoryId,
            OldCost: dto.Cost,
            Cost: dto.Cost,
            OldImagePath: product.ImagePath
        );

        if (req.Image is not null)
        {
            using MemoryStream imageStream = new();
            await req.Image.CopyToAsync(imageStream).ConfigureAwait(false);

            byte[] imageBytes = imageStream.ToArray();
            productEditedDomainEvent = productEditedDomainEvent with
            {
                Image = new(imageBytes, req.Image.FileName, req.Image.ContentType)
            };
        }
        await raiser.RaiseAsync(productEditedDomainEvent).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
