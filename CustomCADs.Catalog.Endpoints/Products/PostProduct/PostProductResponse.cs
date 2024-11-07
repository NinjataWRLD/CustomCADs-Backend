using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Endpoints.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

using static Constants;

public record PostProductResponse(
    Guid Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadDate,
    decimal Cost,
    string CadPath,
    string ImagePath,
    string Status,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CategoryResponse Category
)
{
    public PostProductResponse(GetProductByIdDto dto) : this(
        Id: dto.Id,
        Name: dto.Name,
        Description: dto.Description,
        Cost: dto.Cost,
        Status: dto.Status,
        UploadDate: dto.UploadDate.ToString(DateFormatString),
        ImagePath: dto.ImagePath,
        CadPath: dto.Cad.Path,
        CamCoordinates: new(dto.Cad.CamCoordinates),
        PanCoordinates: new(dto.Cad.PanCoordinates),
        CreatorName: dto.CreatorName,
        Category: new() { Id = dto.Category.Id, Name = dto.Category.Name }
    )
    { }
}
