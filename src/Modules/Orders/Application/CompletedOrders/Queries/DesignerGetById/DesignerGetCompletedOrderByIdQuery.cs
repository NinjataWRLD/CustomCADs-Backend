using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;

public sealed record DesignerGetCompletedOrderByIdQuery(
    CompletedOrderId Id,
    AccountId DesignerId
) : IQuery<DesignerGetCompletedOrderByIdDto>;
