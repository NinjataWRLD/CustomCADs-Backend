using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Endpoints.Categories;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

using static Constants;

public record PostProductResponse(
    ProductId Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadDate,
    MoneyDto Price,
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
        Price: new(dto.Price),
        Status: dto.Status,
        UploadDate: dto.UploadDate.ToString(DateFormatString),
        CamCoordinates: new(dto.Cad.CamCoordinates),
        PanCoordinates: new(dto.Cad.PanCoordinates),
        CreatorName: dto.CreatorName,
        Category: new() { Id = dto.Category.Id.Value, Name = dto.Category.Name }
    )
    { }
}
