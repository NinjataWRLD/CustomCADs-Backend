using CustomCADs.Inventory.Domain.Products.Enums;

namespace CustomCADs.Inventory.Endpoints.Designer.Patch;

public record PatchProductStatusRequest(Guid Id, ProductStatus Status);
