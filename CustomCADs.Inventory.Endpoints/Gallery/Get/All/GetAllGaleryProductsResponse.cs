using CustomCADs.Inventory.Endpoints.Helpers.Dtos;

namespace CustomCADs.Inventory.Endpoints.Gallery.Get.All;

public record GetAllGaleryProductsResponse(
    Guid Id,
    string Name,
    ImageDto Image
);
