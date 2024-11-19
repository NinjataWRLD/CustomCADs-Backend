using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Queries.IsBuyer;

public record IsCartBuyerQuery(CartId Id, UserId UserId) : IQuery<bool>;
