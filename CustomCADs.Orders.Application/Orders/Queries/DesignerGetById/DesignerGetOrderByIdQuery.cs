using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;

public record DesignerGetOrderByIdQuery(
    OrderId Id,
    AccountId DesignerId
) : IQuery<DesignerGetOrderByIdDto>;
