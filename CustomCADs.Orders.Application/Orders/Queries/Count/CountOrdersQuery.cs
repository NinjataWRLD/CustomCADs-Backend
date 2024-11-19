using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Queries.Count;

public record CountOrdersQuery(
    UserId BuyerId,
    OrderStatus Status
) : IQuery<int>;
