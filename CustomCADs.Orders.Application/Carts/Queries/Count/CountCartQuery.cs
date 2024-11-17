using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Carts.Queries.Count;

public record CountCartQuery(UserId BuyerId) : IQuery<int>;
