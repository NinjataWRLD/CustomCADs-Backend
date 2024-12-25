using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public sealed record CreateShipmentCommand(
    ShipmentInfoDto Info,
    AddressDto Address,
    ContactDto Contact,
    AccountId BuyerId
) : ICommand<(ShipmentId Id, decimal Price)>;
