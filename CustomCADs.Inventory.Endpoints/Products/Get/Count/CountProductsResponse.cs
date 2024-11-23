namespace CustomCADs.Inventory.Endpoints.Products.Get.Count;

public record CountProductsResponse(
    int Unchecked,
    int Validated,
    int Reported,
    int Banned
);
