using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public class CreateProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int CategoryId { get; set; }
    public required decimal Cost { get; set; }
    public required ProductStatus Status { get; set; }
    public required Guid CreatorId { get; set; }
}
