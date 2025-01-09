using CustomCADs.Orders.Domain.CompletedOrders.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;

public sealed record GetAllCompletedOrdersQuery(
    Pagination Pagination,
    bool? Delivery = null,
    AccountId? BuyerId = null,
    AccountId? DesignerId = null,
    string? Name = null,
    CompletedOrderSorting? Sorting = null
) : IQuery<Result<GetAllCompletedOrdersDto>>;
