using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public record GetProductGallerySortingsQuery : IQuery<string[]>;
