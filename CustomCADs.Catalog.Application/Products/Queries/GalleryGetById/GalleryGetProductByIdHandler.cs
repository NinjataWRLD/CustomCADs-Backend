﻿using CustomCADs.Catalog.Application.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;

public sealed class GalleryGetProductByIdHandler(IProductReads reads, IRequestSender sender, IEventRaiser raiser)
    : IQueryHandler<GalleryGetProductByIdQuery, GalleryGetProductByIdDto>
{
    public async Task<GalleryGetProductByIdDto> Handle(GalleryGetProductByIdQuery req, CancellationToken ct)
    {
        Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ProductNotFoundException.ById(req.Id);

        if (product.Status is not ProductStatus.Validated)
        {
            throw ProductStatusException.MustBeValidated(req.Id);
        }

        GetUsernameByIdQuery usernameQuery = new(product.CreatorId);
        string username = await sender.SendQueryAsync(usernameQuery, ct).ConfigureAwait(false);

        GetCategoryNameByIdQuery categoryQuery = new(product.CategoryId);
        string categoryName = await sender.SendQueryAsync(categoryQuery, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(product.CreatorId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        if (!req.AccountId.IsEmpty())
        {
            await raiser.RaiseDomainEventAsync(new ProductViewedDomainEvent(
                Id: req.Id,
                AccountId: req.AccountId
            )).ConfigureAwait(false);
        }

        return product.ToGalleryGetProductByIdDto(username, categoryName, timeZone);
    }
}
