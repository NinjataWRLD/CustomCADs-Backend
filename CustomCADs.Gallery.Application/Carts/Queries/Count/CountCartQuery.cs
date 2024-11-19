using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.Count;

public record CountCartQuery(UserId BuyerId) : IQuery<int>;
