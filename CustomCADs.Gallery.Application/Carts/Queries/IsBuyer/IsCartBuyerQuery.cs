using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.IsBuyer;

public record IsCartBuyerQuery(CartId Id, UserId UserId) : IQuery<bool>;
