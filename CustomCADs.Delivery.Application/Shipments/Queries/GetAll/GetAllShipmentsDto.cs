using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetAll;

public record GetAllShipmentsDto(
    ShipmentId Id,
    ShipmentStatus ShipmentStatus,
    Address Address,
    AccountId BuyerId
);