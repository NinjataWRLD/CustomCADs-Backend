using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdDto(Guid? PrevId, GetProductAndAdjacentByIdItemDto Current, Guid? NextId);

public record GetProductAndAdjacentByIdItemDto(Guid Id, Cad Cad);
