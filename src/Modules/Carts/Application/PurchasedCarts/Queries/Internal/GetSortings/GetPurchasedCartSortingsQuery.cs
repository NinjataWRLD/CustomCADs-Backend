using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public record GetPurchasedCartSortingsQuery : IQuery<string[]>;
