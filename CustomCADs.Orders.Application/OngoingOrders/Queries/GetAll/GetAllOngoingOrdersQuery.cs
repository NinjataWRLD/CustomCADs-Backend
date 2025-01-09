using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.GetAll;

public sealed record GetAllOngoingOrdersQuery(
    Pagination Pagination,
    OngoingOrderStatus? OrderStatus = null,
    AccountId? BuyerId = null,
    AccountId? DesignerId = null,
    bool? Delivery = null,
    string? Name = null,
    OngoingOrderSorting? Sorting = null
) : IQuery<Result<GetAllOngoingOrdersDto>>;
