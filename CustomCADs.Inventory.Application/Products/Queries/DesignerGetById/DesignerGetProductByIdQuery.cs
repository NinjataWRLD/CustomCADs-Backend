using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Queries.DesignerGetById;

public record DesignerGetProductByIdQuery(
    ProductId Id,
    AccountId DesignerId
) : IQuery<DesignerGetProductByIdDto>;
