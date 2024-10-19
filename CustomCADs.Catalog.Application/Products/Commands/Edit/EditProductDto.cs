namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public class EditProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int CategoryId { get; set; }
    public required decimal Cost { get; set; }
}
