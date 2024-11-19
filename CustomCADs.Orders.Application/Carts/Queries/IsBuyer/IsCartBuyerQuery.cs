using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Carts.Queries.IsBuyer;

public record IsCartBuyerQuery(CartId Id, UserId UserId) : IQuery<bool>;
