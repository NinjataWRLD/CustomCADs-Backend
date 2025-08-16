using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Shipment.Models;

namespace CustomCADs.Speedy.Core.Contracts.Validation;

internal interface IValidationService
{
	Task<bool> ValidateAddress(AccountModel account, ShipmentAddressModel address, CancellationToken ct = default);
	Task<bool> ValidatePhone(AccountModel account, PhoneNumberModel phoneNumber, CancellationToken ct = default);
	Task<bool> ValidatePostCode(AccountModel account, string postCode, long? siteId = null, CancellationToken ct = default);
	Task<bool> ValidateShipment(AccountModel account, WriteShipmentModel shipment, CancellationToken ct = default);
}
