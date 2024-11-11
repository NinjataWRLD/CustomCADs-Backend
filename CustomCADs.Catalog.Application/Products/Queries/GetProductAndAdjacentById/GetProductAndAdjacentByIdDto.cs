using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdDto(ProductId? PrevId, GetProductAndAdjacentByIdItem Current, ProductId? NextId);

public record GetProductAndAdjacentByIdItem(ProductId Id, Cad Cad);
