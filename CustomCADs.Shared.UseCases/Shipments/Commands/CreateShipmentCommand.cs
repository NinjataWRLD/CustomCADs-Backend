using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Shipments;

namespace CustomCADs.Shared.UseCases.Shipments.Commands;

public record CreateShipmentCommand(UserId ClientId) : ICommand<ShipmentId>;
