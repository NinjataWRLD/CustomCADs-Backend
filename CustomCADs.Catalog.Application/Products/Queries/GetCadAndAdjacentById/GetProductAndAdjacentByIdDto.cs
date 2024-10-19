using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdDto(Guid? PrevId, GetProductAndAdjacentByIdItemDto Current, Guid? NextId);

public class GetProductAndAdjacentByIdItemDto
{
    public Guid Id { get; set; }
    public required Cad Cad { get; set; }
}
