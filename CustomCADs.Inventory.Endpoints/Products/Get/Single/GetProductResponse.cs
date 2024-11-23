using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Single;

public record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    MoneyDto Price,
    string CadKey,
    string UploadDate,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CategoryDto Category
);
