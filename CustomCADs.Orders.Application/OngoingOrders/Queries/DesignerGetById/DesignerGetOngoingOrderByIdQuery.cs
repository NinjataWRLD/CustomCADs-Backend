using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;

public sealed record DesignerGetOngoingOrderByIdQuery(
    OngoingOrderId Id,
    AccountId DesignerId
) : IQuery<DesignerGetOngoingOrderByIdDto>;
