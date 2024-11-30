using CustomCADs.Inventory.Application.Products.Queries;
using CustomCADs.Inventory.Endpoints.Products;

namespace CustomCADs.Inventory.Endpoints.Designer.Get.Single;

public record DesignerSingleProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CadDto Cad,
    CategoryDto Category
);
