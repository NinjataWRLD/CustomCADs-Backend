using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public record GetProductDesignerSortingsQuery : IQuery<string[]>;
