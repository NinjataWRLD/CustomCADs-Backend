using CustomCADs.Catalog.Application.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(int Count, ICollection<GetAllProductsItemDto> Products);

public class GetAllProductsItemDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public required DateTime UploadDate { get; set; }
    public required string ImagePath { get; set; }
    public required string CreatorName { get; set; }
    public CategoryReadDto Category { get; set; } = new() { Name = string.Empty };
}