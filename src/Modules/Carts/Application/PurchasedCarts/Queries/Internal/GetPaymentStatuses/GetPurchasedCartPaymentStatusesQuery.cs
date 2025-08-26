using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

[AddRequestCaching(ExpirationType.Absolute)]
public record GetPurchasedCartPaymentStatusesQuery : IQuery<string[]>;
