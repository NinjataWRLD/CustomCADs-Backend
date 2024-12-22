using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public sealed record CreateShipmentCommand(
    AddressDto Address,
    AccountId BuyerId
) : ICommand<ShipmentId>;
