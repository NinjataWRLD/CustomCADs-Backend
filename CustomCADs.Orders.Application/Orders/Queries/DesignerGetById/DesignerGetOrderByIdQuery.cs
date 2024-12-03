using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;

public sealed record DesignerGetOrderByIdQuery(
    OrderId Id,
    AccountId DesignerId
) : IQuery<DesignerGetOrderByIdDto>;
