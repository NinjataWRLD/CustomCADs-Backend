using CustomCADs.Speedy.Core.Contracts.Shipment;
using CustomCADs.Speedy.Core.Models;

namespace CustomCADs.Speedy.Core.Contracts.Validation;

internal interface IValidationService
{
	Task<bool> ValidateAddress(SpeedyAccount account, ShipmentAddressModel address, CancellationToken ct = default);
	Task<bool> ValidatePhone(SpeedyAccount account, PhoneNumberModel phoneNumber, CancellationToken ct = default);
	Task<bool> ValidatePostCode(SpeedyAccount account, string postCode, long? siteId = null, CancellationToken ct = default);
	Task<bool> ValidateShipment(SpeedyAccount account, WriteShipmentModel shipment, CancellationToken ct = default);
}
