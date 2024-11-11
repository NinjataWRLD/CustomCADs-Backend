using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdDto(ProductId? PrevId, GetProductAndAdjacentByIdItem Current, ProductId? NextId);

public record GetProductAndAdjacentByIdItem(ProductId Id, Cad Cad);
