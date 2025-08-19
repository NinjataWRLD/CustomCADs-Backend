namespace CustomCADs.Speedy.Http.Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;

using AutoSelectNearestOfficePolicy;
using ShipmentAddress;
using ShipmentPhoneNumber;

/// <param name="PrivatePerson">
///     Private person flag.
///     <br />
///     Required: If clientId is provided, forbidden. Otherwise, mandatory.
/// </param>
/// <param name="ContactName">
///     Recipient contact name.
///     <br />
///     Required: No. Forbidden for private persons. Required for companies.
/// </param>
/// <param name="Email">
///     Recipient email.
///     <br />
///     Required: No. Mandatory for international shipments.
/// </param>
/// <param name="ClientId">
///     Client id to refer recipient.
///     <br />
///     Required: No.
/// </param>
/// <param name="ClientName">
///     Recipient customer name.
///     <br />
///     Required: If clientId is provided, forbidden. Otherwise, mandatory.
/// </param>
/// <param name="ObjectName">
///     Recipient customer object.
///     <br />
///     Required: If clientId is provided, forbidden. Otherwise, not mandatory.
/// </param>
/// <param name="PickupOfficeId">
///     Pickup office id.
///     <br />
///     Required: If recipient is internal Speedy customer. If address is provided, forbidden.
/// </param>
/// <param name="PickupGeoPUDOIf">
///     DPD pickup office PUDO id.
///     <br />
///     Required: No. Must be empty if PickupOfficeId is provided.
/// </param>
/// <param name="AutoSelectNearestOffice">
///     Flag for auto-select nearest office.
///     <br />
///     Required: No. Not supported for every destination country.
/// </param>
/// <param name="AutoSelectNearestOfficePolicy">
///     Policy for nearest office auto selection.
///     <br />
///     Required: No. See AutoSelectNearestOfficePolicyDto for details.
/// </param>
/// <param name="Address">
///     Recipient address.
///     <br />
///     Required: If clientId or pickup office is provided, forbidden. Otherwise, mandatory.
///     See ShipmentAddressDto for details.
/// </param>
/// <param name="Phone1">
///     Recipient phone number 1.
///     <br />
///     Required: If shipment is same day delivery, delivery date is half-working day, or delivery country is abroad.
///     See ShipmentPhoneNumberDto for details.
/// </param>
/// <param name="Phone2">
///     Recipient phone number 2.
///     <br />
///     Required: No. See ShipmentPhoneNumberDto for details.
/// </param>
/// <param name="Phone3">
///     Recipient phone number 3.
///     <br />
///     Required: No. See ShipmentPhoneNumberDto for details.
/// </param>
internal record ShipmentRecipientDto(
	bool? PrivatePerson,
	string? ContactName,
	string? Email,
	long? ClientId,
	string? ClientName,
	string? ObjectName,
	int? PickupOfficeId,
	string? PickupGeoPUDOIf,
	bool? AutoSelectNearestOffice,
	AutoSelectNearestOfficePolicyDto? AutoSelectNearestOfficePolicy,
	ShipmentAddressDto? Address,
	ShipmentPhoneNumberDto? Phone1,
	ShipmentPhoneNumberDto? Phone2,
	ShipmentPhoneNumberDto? Phone3
);
