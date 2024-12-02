using CustomCADs.Inventory.Endpoints.Products;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Inventory.Endpoints.Gallery.Get.Single;

public record GetGalleryProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string UploadDate,
    string CadKey,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CategoryDto Category
);
