using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Queries.IsBuyer;

public record IsCartBuyerQuery(CartId Id, UserId UserId) : IQuery<bool>;
