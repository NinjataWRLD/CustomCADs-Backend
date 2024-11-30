using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;

public record DesignerGetOrderByIdQuery(
    OrderId Id,
    AccountId DesignerId
) : IQuery<DesignerGetOrderByIdDto>;
