namespace CustomCADs.Speedy.Http.Dtos.ShipmentSenderAndRecipient.ShipmentSender;

using ShipmentAddress;
using ShipmentPhoneNumber;

/// <param name="Phone1">
///     Sender phone number 1.
///     <br />
///     Required: Yes. See ShipmentPhoneNumberDto for details.
/// </param>
/// <param name="Phone2">
///     Sender phone number 2.
///     <br />
///     Required: No. See ShipmentPhoneNumberDto for details.
/// </param>
/// <param name="Phone3">
///     Sender phone number 3.
///     <br />
///     Required: No. See ShipmentPhoneNumberDto for details.
/// </param>
/// <param name="Address">
///     Sender address.
///     <br />
///     Required: Yes, unless DropoffOfficeId is provided. See ShipmentAddressDto for details.
/// </param>
/// <param name="Email">
///     Sender email.
///     <br />
///     Required: No. Mandatory for international shipments.
/// </param>
/// <param name="ClientId">
///     Client id to refer sender.
///     <br />
///     Required: No.
/// </param>
/// <param name="ClientName">
///     Sender customer name.
///     <br />
///     Required: If clientId is provided, forbidden. Otherwise, mandatory.
/// </param>
/// <param name="ContactName">
///     Sender contact name.
///     <br />
///     Required: No. Forbidden for private persons. Required for companies.
/// </param>
/// <param name="PrivatePerson">
///     Private person flag.
///     <br />
///     Required: If clientId is provided, forbidden. Otherwise, mandatory.
/// </param>
/// <param name="DropoffOfficeId">
///     Dropoff office id.
///     <br />
///     Required: If sender is internal Speedy customer. If address is provided, forbidden.
/// </param>
/// <param name="DropoffGeoPUDOId">
///     DPD dropoff office PUDO id.
///     <br />
///     Required: No. Must be empty if DropoffOfficeId is provided.
/// </param>
internal record ShipmentSenderDto(
	ShipmentPhoneNumberDto Phone1,
	ShipmentPhoneNumberDto? Phone2,
	ShipmentPhoneNumberDto? Phone3,
	ShipmentAddressDto? Address,
	string? Email,
	long? ClientId,
	string? ClientName,
	string? ContactName,
	bool? PrivatePerson,
	int? DropoffOfficeId,
	string? DropoffGeoPUDOId
);
