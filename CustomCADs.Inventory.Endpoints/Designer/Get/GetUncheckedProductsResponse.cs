namespace CustomCADs.Inventory.Endpoints.Designer.Get;

public record GetUncheckedProductsResponse(
    int Count,
    ICollection<GetUncheckedProductsDto> Products
);
