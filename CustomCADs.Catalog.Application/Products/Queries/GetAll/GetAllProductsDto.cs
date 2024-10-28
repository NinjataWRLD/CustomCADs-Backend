using CustomCADs.Catalog.Application.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(int Count, ICollection<GetAllProductsItemDto> Products);

public class GetAllProductsItemDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string UploadDate { get; set; }
    public required string ImagePath { get; set; }
    public CategoryReadDto Category { get; set; } = new() { Name = string.Empty };
}