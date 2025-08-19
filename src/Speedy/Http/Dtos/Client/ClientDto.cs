namespace CustomCADs.Speedy.Http.Dtos.Client;

using ShipmentSenderAndRecipient.ShipmentAddress;

/// <param name="ClientId">
///     Client id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ClientName">
///     Client name.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ObjectName">
///     Object name.
///     <br />
///     Required: No.
/// </param>
/// <param name="ContactName">
///     Contact name.
///     <br />
///     Required: No.
/// </param>
/// <param name="Address">
///     Client address.
///     <br />
///     Required: Yes. See AddressDto for details.
/// </param>
/// <param name="Email">
///     Client email.
///     <br />
///     Required: No.
/// </param>
/// <param name="PrivatePerson">
///     Private person flag.
///     <br />
///     Required: Yes.
/// </param>
internal record ClientDto(
	long ClientId,
	string ClientName,
	string ObjectName,
	string ContactName,
	AddressDto Address,
	string Email,
	bool PrivatePerson
);
