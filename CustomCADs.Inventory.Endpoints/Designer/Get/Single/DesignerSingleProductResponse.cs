using CustomCADs.Inventory.Application.Products.Queries;
using CustomCADs.Inventory.Endpoints.Helpers.Dtos;

namespace CustomCADs.Inventory.Endpoints.Designer.Get.Single;

public sealed record DesignerSingleProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CadDto Cad,
    CategoryResponse Category
);
