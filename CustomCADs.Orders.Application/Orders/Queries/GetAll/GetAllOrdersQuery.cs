using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Domain.Orders.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public record GetAllOrdersQuery(
    DeliveryType? DeliveryType = null,
    OrderStatus? OrderStatus = null,
    UserId? BuyerId = null,
    UserId? DesignerId = null,
    string? Name = null,
    OrderSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<Result<GetAllOrdersDto>>;
