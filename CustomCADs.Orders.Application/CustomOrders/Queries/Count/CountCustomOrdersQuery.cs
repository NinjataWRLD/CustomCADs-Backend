using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.Count;

public record CountCustomOrdersQuery(
    UserId BuyerId,
    CustomOrderStatus Status
) : IQuery<int>;
