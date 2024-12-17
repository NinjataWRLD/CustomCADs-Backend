using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public sealed record GetAllOrdersQuery(
    Pagination Pagination,
    bool? Delivery = null,
    OrderStatus? OrderStatus = null,
    AccountId? BuyerId = null,
    AccountId? DesignerId = null,
    string? Name = null,
    OrderSorting? Sorting = null
) : IQuery<Result<GetAllOrdersDto>>;
