using CustomCADs.Catalog.Application.Products.Queries;
using CustomCADs.Catalog.Endpoints.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Single;

public sealed record DesignerSingleProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CadDto Cad,
    CategoryResponse Category
);
