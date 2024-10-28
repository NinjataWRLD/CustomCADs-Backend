using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public class GetProductByIdDto()
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Cost { get; set; }
    public required Cad Cad { get; set; }
    public required string CreationDate { get; set; }
    public CategoryReadDto Category { get; set; } = new() { Name = string.Empty };
}
