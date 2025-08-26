using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public record GetShipmentSortingsQuery : IQuery<string[]>;
