using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.IsBuyer;

public record IsCustomOrderBuyerQuery(CustomOrderId Id, UserId UserId) : IQuery<bool>;
