namespace CustomCADs.Inventory.Endpoints.Designer.Get.All;

public record GetUncheckedProductsResponse(
    int Count,
    ICollection<GetUncheckedProductsDto> Products
);
