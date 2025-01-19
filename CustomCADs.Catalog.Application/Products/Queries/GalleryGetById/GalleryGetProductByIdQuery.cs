using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;

public sealed record GalleryGetProductByIdQuery(
    ProductId Id,
    AccountId AccountId
) : IQuery<GalleryGetProductByIdDto>;
