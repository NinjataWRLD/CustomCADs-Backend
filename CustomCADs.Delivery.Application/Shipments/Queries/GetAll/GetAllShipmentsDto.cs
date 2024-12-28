using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetAll;

public record GetAllShipmentsDto(
    ShipmentId Id,
    Address Address,
    AccountId BuyerId
);