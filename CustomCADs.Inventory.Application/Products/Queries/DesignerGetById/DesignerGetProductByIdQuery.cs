using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.DesignerGetById;

public record DesignerGetProductByIdQuery(
    ProductId Id,
    AccountId DesignerId
) : IQuery<DesignerGetProductByIdDto>;
