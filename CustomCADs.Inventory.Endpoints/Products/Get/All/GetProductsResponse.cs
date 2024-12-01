namespace CustomCADs.Inventory.Endpoints.Products.Get.All;

public record GetProductsResponse(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    ImageDto Image,
    CategoryDto Category
);
