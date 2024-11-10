using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Endpoints.Categories;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.GetProduct;

using static Constants;

public record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    MoneyDto Price,
    string CadPath,
    string UploadDate,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CategoryResponse Category
)
{
    public GetProductResponse(GetProductByIdDto dto) : this(
        Id: dto.Id,
        Name: dto.Name,
        Price: new(dto.Price),
        Description: dto.Description,
        UploadDate: dto.UploadDate.ToString(DateFormatString),
        CamCoordinates: new(dto.Cad.CamCoordinates),
        PanCoordinates: new(dto.Cad.PanCoordinates),
        CadPath: dto.Cad.Path,
        Category: new() { Id = dto.Category.Id, Name = dto.Category.Name }
    )
    { }
}
